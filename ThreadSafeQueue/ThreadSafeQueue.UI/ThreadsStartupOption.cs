namespace ThreadSafeQueue.Logic
{
    /// <summary>
    /// Опции запуска демонстрационных потоков
    /// </summary>
    public enum ThreadsStartupOption
    {
        /// <summary>
        /// Запускаются демонстрационные потоки содержащие строковые операции
        /// </summary>
        String,

        /// <summary>
        /// Запускаются демонстрационные потоки содержащие численные (целочисленные) операции
        /// </summary>
        Integer,

        /// <summary>
        /// Запускаются демонстрационные потоки содержащие символьные операции
        /// </summary>
        Char
    }
}
