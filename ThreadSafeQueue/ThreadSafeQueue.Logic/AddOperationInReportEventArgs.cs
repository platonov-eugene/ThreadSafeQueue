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
    public class AddOperationInReportEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public Operation Operation { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        public AddOperationInReportEventArgs(Operation operation)
        {
            Operation = operation;
        }
    }
}
