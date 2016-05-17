using System;

namespace ThreadSafeQueue.Logic
{
    /// <summary>
    /// Аргументы события добавления сведений о выполненной операции в отчет
    /// </summary>
    public class AddOperationInReportEventArgs : EventArgs
    {
        /// <summary>
        /// Сведения о выполненной операции
        /// </summary>
        public Operation Operation { get; private set; }

        /// <summary>
        /// Конструктор класса содержащий аргументы события добавления сведений о выполненной операции в отчет
        /// </summary>
        /// <param name="operation">Сведения о выполненной операции</param>
        public AddOperationInReportEventArgs(Operation operation)
        {
            Operation = operation;
        }
    }
}
