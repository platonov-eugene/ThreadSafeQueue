namespace ThreadSafeQueue.Logic
{
    /// <summary>
    /// Опции запуска демонстрационных потоков
    /// </summary>
    public enum ThreadsStartupOption
    {
        /// <summary>
        /// Запускаются демонстрационные потоки содержащие строковое операции
        /// </summary>
        String,

        /// <summary>
        /// Запускаются демонстрационне потоки содержащие численные (целочисленные) операции
        /// </summary>
        Integer,

        /// <summary>
        /// Запускаются демонстрационные потоки содержащие символьные операции
        /// </summary>
        Char
    }
}
