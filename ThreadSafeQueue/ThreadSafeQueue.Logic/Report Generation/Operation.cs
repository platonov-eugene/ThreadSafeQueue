using System;

namespace ThreadSafeQueue.Logic
{
    /// <summary>
    /// Сведения о выполненной операции
    /// </summary>
    public class Operation
    {
        /// <summary>
        /// Дата и время выполнения операции
        /// </summary>
        public DateTime DateTime { get; private set; }

        /// <summary>
        /// Сведения об элементах содержащихся до начала операции
        /// </summary>
        public string ItemsBeforeOperation { get; private set; }

        /// <summary>
        /// Наименование операции
        /// </summary>
        public string NameOfOperation { get; private set; }

        /// <summary>
        /// Сведения об элементах содержащихся по окончанию операции
        /// </summary>
        public string ItemsAfterOperation { get; private set; }

        /// <summary>
        /// Конструктор объекта содержащий сведения о выполненной операции
        /// </summary>
        /// <param name="itemsBeforeOperation">Сведения об элементах содержащихся до начала операции</param>
        /// <param name="nameOfOperation">Наименование операции</param>
        /// <param name="itemsAfterOperation">Сведения об элементах содержащихся по окончанию операции</param>
        public Operation(string itemsBeforeOperation, string nameOfOperation, string itemsAfterOperation)
        {
            DateTime = DateTime.Now;
            ItemsBeforeOperation = itemsBeforeOperation;
            NameOfOperation = nameOfOperation;
            ItemsAfterOperation = itemsAfterOperation;
        }

        /// <summary>
        /// Возвращает сведения о выполненной операции в строковом преставлении
        /// </summary>
        /// <returns>Строка содержащая сведения о выполненной операции</returns>
        public override string ToString()
        {
            return string.Format("- {0} {1} -\nОчередь: {2}\nДействие: {3}\nОчередь: {4}\n",
                                 DateTime.ToShortDateString(), DateTime.ToLongTimeString(), ItemsBeforeOperation, NameOfOperation, ItemsAfterOperation);
        }
    }
}
