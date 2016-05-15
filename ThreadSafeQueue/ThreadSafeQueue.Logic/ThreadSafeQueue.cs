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
                string itemsBeforeOperation = ToString();

                while (queue.Count == 0)
                    Monitor.Wait(queue);
                T result = queue.Dequeue();

                string name = Thread.CurrentThread.Name + " - " + GetAbbreviatedNameOfType(result) + " Pop()";
                string itemsAfterOperation = ToString();
                report.Add(itemsBeforeOperation, name, itemsAfterOperation);

                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetAbbreviatedNameOfType(T item)
        {
            switch (item.GetType().FullName)
            {
                case "System.String":
                    return "string";
                case "System.Char":
                    return "char";
                case "System.Int32":
                    return "int";
                default:
                    return item.GetType().FullName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string ToString(T item)
        {
            switch (item.GetType().FullName)
            {
                case "System.String":
                    return "\"" + item + "\" ";
                case "System.Char":
                    return "\'" + item + "\' ";
                default:
                    return item + " ";
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

                foreach (T item in queue)
                {
                    switch (item.GetType().FullName)
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
