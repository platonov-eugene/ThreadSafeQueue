using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadSafeQueue.Logic
{
    /// <summary>
    /// 
    /// </summary>
    public class Operation
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime DateTime { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string ItemsBeforeOperation { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string ItemsAfterOperation { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemsBeforeOperation"></param>
        /// <param name="name"></param>
        /// <param name="itemsAfterOperation"></param>
        public Operation(string itemsBeforeOperation, string name, string itemsAfterOperation)
        {
            DateTime = DateTime.Now;
            ItemsBeforeOperation = itemsBeforeOperation;
            Name = name;
            ItemsAfterOperation = itemsAfterOperation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("- {0} {1} -\nОчередь: {2}\nДействие: {3}\nОчередь: {4}\n",
                                 DateTime.ToShortDateString(), DateTime.ToLongTimeString(), ItemsBeforeOperation, Name, ItemsAfterOperation);
        }
    }
}
