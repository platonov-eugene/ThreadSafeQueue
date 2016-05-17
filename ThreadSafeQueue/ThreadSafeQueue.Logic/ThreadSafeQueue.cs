using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ThreadSafeQueue.Logic
{
    /// <summary>
    /// Потокобезопасная очередь элементов ThreadSafeQueue
    /// </summary>
    /// <typeparam name="T">Тип элементов в потокобезопасной очереди элементов</typeparam>
    public class ThreadSafeQueue<T>
    {
        /// <summary>
        /// Объект блокировки
        /// </summary>
        private object thisLock = new object();

        /// <summary>
        /// Очередь элементов, основанная на принципе "первым вошел - первым вышел"
        /// </summary>
        private readonly Queue<T> queue = new Queue<T>();

        /// <summary>
        /// Отчет содержащий последовательность выполненных операций
        /// </summary>
        private readonly Report report = new Report();

        /// <summary>
        /// Конструктор потокобезопасной очереди элементов ThreadSafeQueue
        /// </summary>
        /// <param name="report">Отчет осуществляющий формирование последовательности о выполненных операциях</param>
        public ThreadSafeQueue(Report report)
        {
            this.report = report;
        }

        /// <summary>
        /// Добавляет элемент типа T в конец коллекции потокобезопасной очереди элементов ThreadSafeQueue
        /// </summary>
        /// <param name="item">Добавляемый элемент типа T в конец коллекции потокобезопасной очереди элементов ThreadSafeQueue</param>
        public void Push(T item)
        {
            lock (thisLock)
            {
                string itemsBeforeOperation = ToString();

                queue.Enqueue(item);

                string nameOfOperation = Thread.CurrentThread.Name + " - void Push(" + ToString(item) + ")";
                string itemsAfterOperation = ToString();
                report.AddOperation(itemsBeforeOperation, nameOfOperation, itemsAfterOperation);

                if (queue.Count == 1)
                    Monitor.PulseAll(thisLock);
            }
        }

        /// <summary>
        /// Удаляет и возвращает элемент типа T, находящийся в начале потокобезопасной очереди элементов ThreadSafeQueue. В случае,
        /// отсутствия элементов типа T в потокобезопасной очереди элементов ThreadSafeQueue, осуществляет ожидание добавления элемента
        /// типа T в потокобезопасную очередь элементов ThreadSafeQueue.
        /// </summary>
        /// <returns>Элемент типа T, находящийся в начале потокобезопасной очереди элементов ThreadSafeQueue</returns>
        public T Pop()
        {
            lock (thisLock)
            {
                string itemsBeforeOperation = string.Empty;
                string nameOfOperation = string.Empty;
                string itemsAfterOperation = string.Empty;

                bool isWaitingThread = false;

                while (queue.Count == 0)
                {
                    if (!isWaitingThread)
                    {
                        itemsBeforeOperation = ToString();
                        nameOfOperation = Thread.CurrentThread.Name + " - " + GetCSharpTypeName() + " Pop()";
                        itemsAfterOperation = "ожидание нового элемента";
                        report.AddOperation(itemsBeforeOperation, nameOfOperation, itemsAfterOperation);
                    }

                    isWaitingThread = true;
                    Monitor.Wait(thisLock);
                }

                itemsBeforeOperation = ToString();

                T result = queue.Dequeue();

                itemsAfterOperation = ToString();

                if (isWaitingThread)
                    nameOfOperation = "Ожидающий " + ToLowerFirstCharacter(Thread.CurrentThread.Name) + " - " + GetCSharpTypeName() + " Pop()";
                else
                    nameOfOperation = Thread.CurrentThread.Name + " - " + GetCSharpTypeName() + " Pop()";

                report.AddOperation(itemsBeforeOperation, nameOfOperation, itemsAfterOperation);

                return result;
            }
        }

        /// <summary>
        /// Возвращает указанное наименование потока начинающиеся с буквы в нижнем регистре
        /// </summary>
        /// <param name="threadName">Наименование потока</param>
        /// <returns>Наименование потока начинающиеся с буквы в нижнем регистре</returns>
        public string ToLowerFirstCharacter(string threadName)
        {
            if (string.IsNullOrEmpty(threadName))
                return threadName;

            if (threadName.Length == 1)
                return threadName[0].ToString().ToLower();

            return threadName[0].ToString().ToLower() + threadName.Substring(1);
        }

        /// <summary>
        /// Возвращает псевдоним для типа T элементов содержащихся в потокобезопасной очереди элементов
        /// </summary>
        /// <returns>Псевдоним для типа T элементов содержащихся в потокобезопасной очереди элементов</returns>
        public string GetCSharpTypeName()
        {
            string fullName = typeof(T).FullName;

            switch (fullName)
            {
                case "System.String":
                    return "string";
                case "System.Char":
                    return "char";
                case "System.Int32":
                    return "int";
                default:
                    return fullName;
            }
        }

        /// <summary>
        /// Возвращает строковое представление элемента типа T
        /// </summary>
        /// <param name="item">Элемент типа T</param>
        /// <returns>Строковое представление элемента типа T</returns>
        public string ToString(T item)
        {
            switch (typeof(T).FullName)
            {
                case "System.String":
                    return "\"" + item + "\"";
                case "System.Char":
                    return "\'" + item + "\'";
                default:
                    return item.ToString();
            }
        }

        /// <summary>
        /// Возвращает строкое представление элементов типа T содержащихся в потокобезопасной очереди элементов в обратном порядке
        /// </summary>
        /// <returns>Строковое представление элементов типа T содержащихся в потокобезопасной очереди элементов в обратном порядке</returns>
        public override string ToString()
        {
            if (queue.Count == 0)
                return "пустая";
            else
            {
                string result = string.Empty;

                for (int index = queue.Count - 1; index >= 0; index--)
                    result += ToString(queue.ElementAt(index)) + " ";

                return result;
            }
        }
    }
}
