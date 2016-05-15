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
    public class Report
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly List<Operation> operations = new List<Operation>();

        /// <summary>
        /// 
        /// </summary>
        public delegate void AddOperationInReportEventHandler(object sender, AddOperationInReportEventArgs e);

        /// <summary>
        /// 
        /// </summary>
        public event AddOperationInReportEventHandler AddOperationInReport;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemsBeforeOperation"></param>
        /// <param name="name"></param>
        /// <param name="itemsAfterOperation"></param>
        public void Add(string itemsBeforeOperation, string name, string itemsAfterOperation)
        {
            Operation operation = new Operation(itemsBeforeOperation, name, itemsAfterOperation);
            operations.Add(operation);
            AddOperationInReport(this, new AddOperationInReportEventArgs(operation));
        }
    }
}
