using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSafeQueue.Logic
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ThreadSafeQueue<T>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly Queue<T> queue = new Queue<T>();

        /// <summary>
        /// 
        /// </summary>
        private readonly Report report = new Report();

        /// <summary>
        /// 
        /// </summary>
        public ThreadSafeQueue()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="report"></param>
        public ThreadSafeQueue(Report report)
        {
            this.report = report;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Push(T item)
        {
            lock (queue)
            {
                string itemsBeforeOperation = ToString();

                queue.Enqueue(item);

                string name = Thread.CurrentThread.Name + " - void Push(" + ToString(item) + ")";
                string itemsAfterOperation = ToString();
                report.Add(itemsBeforeOperation, name, itemsAfterOperation);

                if (queue.Count == 1)
                    Monitor.PulseAll(queue);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            lock (queue)
            {
                bool isWaiting = false;

                while (queue.Count == 0)
                {
                    string itemsBeforeOperation = ToString();
                    string name = Thread.CurrentThread.Name + " - " + GetCSharpTypeName() + " Pop()";
                    string itemsAfterOperation = "ожидание нового элемента";
                    report.Add(itemsBeforeOperation, name, itemsAfterOperation);

                    isWaiting = true;

                    Monitor.Wait(queue);
                }

                T result;

                if (isWaiting)
                {
                    string itemsBeforeOperation = ToString();
                    result = queue.Dequeue();

                    string name = "Ожидающий " + ToLowerFirstCharacter(Thread.CurrentThread.Name) + " - " + GetCSharpTypeName() + " Pop()";
                    string itemsAfterOperation = ToString();
                    report.Add(itemsBeforeOperation, name, itemsAfterOperation);
                }
                else
                {
                    string itemsBeforeOperation = ToString();
                    result = queue.Dequeue();

                    string name = Thread.CurrentThread.Name + " - " + GetCSharpTypeName() + " Pop()";
                    string itemsAfterOperation = ToString();
                    report.Add(itemsBeforeOperation, name, itemsAfterOperation);
                }

                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string ToLowerFirstCharacter(string name)
        {
            if (string.IsNullOrEmpty(name) || name.Length == 1)
                return name;

            return name[0].ToString().ToLower() + name.Substring(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (queue.Count == 0)
                return "пустая";
            else
            {
                string result = string.Empty;

                for (int index = queue.Count - 1; index >= 0; index--)
                {
                    T item = queue.ElementAt(index);
                    switch (typeof(T).FullName)
                    {
                        case "System.String":
                            result += "\"" + item + "\" ";
                            break;
                        case "System.Char":
                            result += "\'" + item + "\' ";
                            break;
                        default:
                            result += item + " ";
                            break;
                    }
                }

                return result;
            }
        }
    }
}
