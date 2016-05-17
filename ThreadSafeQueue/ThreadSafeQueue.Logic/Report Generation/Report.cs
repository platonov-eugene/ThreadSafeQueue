using System.Collections.Generic;

namespace ThreadSafeQueue.Logic
{
    /// <summary>
    /// Отчет содержащий последовательность выполненных операций
    /// </summary>
    public class Report
    {
        /// <summary>
        /// Последовательность выполненных операций
        /// </summary>
        private readonly List<Operation> operations = new List<Operation>();

        /// <summary>
        /// Обработчик события добавления сведений о выполненной операции в отчет
        /// </summary>
        /// <param name="sender">Объект, к которому присоединен обработчик события</param>
        /// <param name="e">Сведения об аргументах события добавления сведений о выполненной операции в отчет</param>
        public delegate void AddOperationInReportEventHandler(object sender, AddOperationInReportEventArgs e);

        /// <summary>
        /// Событие возникающее при добавлении сведений о выполненной операции в отчет
        /// </summary>
        public event AddOperationInReportEventHandler AddOperationInReport;

        /// <summary>
        /// Добавление сведений о выполненной операции в отчет
        /// </summary>
        /// <param name="itemsBeforeOperation">Сведения об элементах содержащихся до начала операции</param>
        /// <param name="nameOfOperation">Наименование операции</param>
        /// <param name="itemsAfterOperation">Сведения об элементах содержащихся по окончанию операции</param>
        public void AddOperation(string itemsBeforeOperation, string nameOfOperation, string itemsAfterOperation)
        {
            Operation operation = new Operation(itemsBeforeOperation, nameOfOperation, itemsAfterOperation);
            operations.Add(operation);
            AddOperationInReport(this, new AddOperationInReportEventArgs(operation));
        }
    }
}
