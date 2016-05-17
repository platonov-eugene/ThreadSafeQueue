using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using ThreadSafeQueue.Logic;

namespace ThreadSafeQueue.UI
{
    /// <summary>
    /// Форма приложения
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Опция запуска демонстрационных потоков
        /// </summary>
        private ThreadsStartupOption ThreadsStartupOption { get; set; }

        /// <summary>
        /// Демонстрационные потоки приложения
        /// </summary>
        private Thread[] Threads = new Thread[4];

        /// <summary>
        /// Конструктор по умолчанию формы приложения
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            LoadStringOperationsInThreads(buttonLoadStringOperations, new RoutedEventArgs());
        }

        #region Включение и выключение потоков

        private void SwitchingOnAndOffThreads(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            bool isEnabledThread = (button.Content.ToString() == "Включен");

            button.Content = isEnabledThread ? "Выключен" : "Включен";
            button.Foreground = isEnabledThread ? Brushes.Red : Brushes.Black;
            isEnabledThread = !isEnabledThread;

            if (button.Name == "buttonSwitchingOnAndOffThreadA")
            {
                labelHeaderThreadA.IsEnabled = isEnabledThread;
                richtextboxOperationInThreadA.IsEnabled = isEnabledThread;
                labelIntervalRunningOperationsInThreadA.IsEnabled = isEnabledThread;
                comboboxIntervalRunningOperationsInThreadA.IsEnabled = isEnabledThread;
            }
            else if (button.Name == "buttonSwitchingOnAndOffThreadB")
            {
                labelHeaderThreadB.IsEnabled = isEnabledThread;
                richtextboxOperationInThreadB.IsEnabled = isEnabledThread;
                labelIntervalRunningOperationsInThreadB.IsEnabled = isEnabledThread;
                comboboxIntervalRunningOperationsInThreadB.IsEnabled = isEnabledThread;
            }
            else if (button.Name == "buttonSwitchingOnAndOffThreadC")
            {
                labelHeaderThreadC.IsEnabled = isEnabledThread;
                richtextboxOperationInThreadC.IsEnabled = isEnabledThread;
                labelIntervalRunningOperationsInThreadC.IsEnabled = isEnabledThread;
                comboboxIntervalRunningOperationsInThreadC.IsEnabled = isEnabledThread;
            }
            else if (button.Name == "buttonSwitchingOnAndOffThreadD")
            {
                labelHeaderThreadD.IsEnabled = isEnabledThread;
                richtextboxOperationInThreadD.IsEnabled = isEnabledThread;
                labelIntervalRunningOperationsInThreadD.IsEnabled = isEnabledThread;
                comboboxIntervalRunningOperationsInThreadD.IsEnabled = isEnabledThread;
            }
        }

        #endregion

        #region Запуск потоков

        private void EnableAndCleanAllThreads()
        {
            labelHeaderThreadA.IsEnabled = true;
            richtextboxOperationInThreadA.IsEnabled = true;
            richtextboxOperationInThreadA.Document.Blocks.Clear();
            labelIntervalRunningOperationsInThreadA.IsEnabled = true;
            comboboxIntervalRunningOperationsInThreadA.IsEnabled = true;
            comboboxIntervalRunningOperationsInThreadA.SelectedIndex = -1;
            buttonSwitchingOnAndOffThreadA.Content = "Включен";
            buttonSwitchingOnAndOffThreadA.Foreground = Brushes.Black;

            labelHeaderThreadB.IsEnabled = true;
            richtextboxOperationInThreadB.IsEnabled = true;
            richtextboxOperationInThreadB.Document.Blocks.Clear();
            labelIntervalRunningOperationsInThreadB.IsEnabled = true;
            comboboxIntervalRunningOperationsInThreadB.IsEnabled = true;
            comboboxIntervalRunningOperationsInThreadB.SelectedIndex = -1;
            buttonSwitchingOnAndOffThreadB.Content = "Включен";
            buttonSwitchingOnAndOffThreadB.Foreground = Brushes.Black;

            labelHeaderThreadC.IsEnabled = true;
            richtextboxOperationInThreadC.IsEnabled = true;
            richtextboxOperationInThreadC.Document.Blocks.Clear();
            labelIntervalRunningOperationsInThreadC.IsEnabled = true;
            comboboxIntervalRunningOperationsInThreadC.IsEnabled = true;
            comboboxIntervalRunningOperationsInThreadC.SelectedIndex = -1;
            buttonSwitchingOnAndOffThreadC.Content = "Включен";
            buttonSwitchingOnAndOffThreadC.Foreground = Brushes.Black;

            labelHeaderThreadD.IsEnabled = true;
            richtextboxOperationInThreadD.IsEnabled = true;
            richtextboxOperationInThreadD.Document.Blocks.Clear();
            labelIntervalRunningOperationsInThreadD.IsEnabled = true;
            comboboxIntervalRunningOperationsInThreadD.IsEnabled = true;
            comboboxIntervalRunningOperationsInThreadD.SelectedIndex = -1;
            buttonSwitchingOnAndOffThreadD.Content = "Включен";
            buttonSwitchingOnAndOffThreadD.Foreground = Brushes.Black;
        }

        private void RunThreads(object sender, RoutedEventArgs e)
        {
            if (!labelHeaderThreadA.IsEnabled && !labelHeaderThreadB.IsEnabled && !labelHeaderThreadC.IsEnabled && !labelHeaderThreadD.IsEnabled)
                MessageBox.Show("Для успешного запуска потоков необходимо выполнить включение в работу как минимум одного из потоков.", "Ошибка при запуске потоков", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                richtextboxSequenceOperations.Document.Blocks.Clear();

                if (ThreadsStartupOption == ThreadsStartupOption.String)
                    RunThreadsWithStringOperations();
                else if (ThreadsStartupOption == ThreadsStartupOption.Integer)
                    RunThreadsWithIntegerOperations();
                else if (ThreadsStartupOption == ThreadsStartupOption.Char)
                    RunThreadsWithCharacterOperations();
            }
        }

        private void AddOperationInReport(object sender, AddOperationInReportEventArgs e)
        {
            richtextboxSequenceOperations.Dispatcher.Invoke(new Action(() =>
            {
                if (richtextboxSequenceOperations.Document.Blocks.Count == 0)
                    richtextboxSequenceOperations.AppendText(e.Operation.ToString() + "\n");
                else
                    richtextboxSequenceOperations.AppendText(e.Operation.ToString());

                int count = richtextboxSequenceOperations.Document.Blocks.Count;
                richtextboxSequenceOperations.Document.Blocks.ElementAt(count - 5).FontWeight = FontWeights.DemiBold;
                richtextboxSequenceOperations.Document.Blocks.ElementAt(count - 5).TextAlignment = TextAlignment.Center;

                Paragraph paragraph = (Paragraph)richtextboxSequenceOperations.Document.Blocks.ElementAt(count - 3);
                if (new TextRange(paragraph.ContentStart, paragraph.ContentEnd).Text.Contains("Push"))
                    richtextboxSequenceOperations.Document.Blocks.ElementAt(count - 3).Foreground = Brushes.Green;
                else
                {
                    paragraph = (Paragraph)richtextboxSequenceOperations.Document.Blocks.ElementAt(count - 2);
                    if (new TextRange(paragraph.ContentStart, paragraph.ContentEnd).Text.Contains("ожидание нового элемента"))
                        richtextboxSequenceOperations.Document.Blocks.ElementAt(count - 3).Foreground = Brushes.Red;
                    else
                        richtextboxSequenceOperations.Document.Blocks.ElementAt(count - 3).Foreground = Brushes.Blue;
                }

                scrollViewerSequenceOperations.ScrollToEnd();
            }));
        }

        #endregion

        #region Запуск потоков содержащих строковые операции

        private void RunThreadsWithStringOperations()
        {
            foreach (Thread thread in Threads)
                if (thread != null && thread.IsAlive)
                    thread.Abort();

            Report report = new Report();
            report.AddOperationInReport += AddOperationInReport;

            ThreadSafeQueue<string> threadSafeQueue = new ThreadSafeQueue<string>(report);

            Thread threadA = new Thread(ThreadAWithStringOperations);
            threadA.Name = "Поток А";

            Thread threadB = new Thread(ThreadBWithStringOperations);
            threadB.Name = "Поток B";

            Thread threadC = new Thread(ThreadCWithStringOperations);
            threadC.Name = "Поток C";

            Thread threadD = new Thread(ThreadDWithStringOperations);
            threadD.Name = "Поток D";

            Threads =  new Thread[] { threadA, threadB, threadC, threadD };

            if (richtextboxOperationInThreadA.IsEnabled)
                threadA.Start(threadSafeQueue);

            if (richtextboxOperationInThreadB.IsEnabled)
                threadB.Start(threadSafeQueue);

            if (richtextboxOperationInThreadC.IsEnabled)
                threadC.Start(threadSafeQueue);

            if (richtextboxOperationInThreadD.IsEnabled)
                threadD.Start(threadSafeQueue);
        }

        private void ThreadAWithStringOperations(object data)
        {
            int intervalRunningOperationsInThreadA = 0;
            comboboxIntervalRunningOperationsInThreadA.Dispatcher.Invoke(new Action(() => 
            {
                intervalRunningOperationsInThreadA = (comboboxIntervalRunningOperationsInThreadA.SelectedIndex + 1) * 1000;
            }));

            ThreadSafeQueue<string> threadSafeQueue = data as ThreadSafeQueue<string>;

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadA);

            threadSafeQueue.Push("ABC");
            Thread.Sleep(intervalRunningOperationsInThreadA);

            threadSafeQueue.Push("DEF");
            Thread.Sleep(intervalRunningOperationsInThreadA);

            threadSafeQueue.Push("GHI");
            Thread.Sleep(intervalRunningOperationsInThreadA);

            threadSafeQueue.Push("JKL");
            Thread.Sleep(intervalRunningOperationsInThreadA);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadA);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadA);

            threadSafeQueue.Push("MNO");
            Thread.Sleep(intervalRunningOperationsInThreadA);
        }

        private void ThreadBWithStringOperations(object data)
        {
            int intervalRunningOperationsInThreadB = 0;
            comboboxIntervalRunningOperationsInThreadB.Dispatcher.Invoke(new Action(() =>
            {
                intervalRunningOperationsInThreadB = (comboboxIntervalRunningOperationsInThreadB.SelectedIndex + 1) * 1000;
            }));

            ThreadSafeQueue<string> threadSafeQueue = data as ThreadSafeQueue<string>;

            threadSafeQueue.Push("PQR");
            Thread.Sleep(intervalRunningOperationsInThreadB);

            threadSafeQueue.Push("STU");
            Thread.Sleep(intervalRunningOperationsInThreadB);

            threadSafeQueue.Push("VMX");
            Thread.Sleep(intervalRunningOperationsInThreadB);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadB);

            threadSafeQueue.Push("YZA");
            Thread.Sleep(intervalRunningOperationsInThreadB);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadB);

            threadSafeQueue.Push("AGM");
            Thread.Sleep(intervalRunningOperationsInThreadB);

            threadSafeQueue.Push("BHN");
            Thread.Sleep(intervalRunningOperationsInThreadB);
        }

        private void ThreadCWithStringOperations(object data)
        {
            int intervalRunningOperationsInThreadC = 0;
            comboboxIntervalRunningOperationsInThreadC.Dispatcher.Invoke(new Action(() =>
            {
                intervalRunningOperationsInThreadC = (comboboxIntervalRunningOperationsInThreadC.SelectedIndex + 1) * 1000;
            }));

            ThreadSafeQueue<string> threadSafeQueue = data as ThreadSafeQueue<string>;

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadC);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadC);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadC);

            threadSafeQueue.Push("CIO");
            Thread.Sleep(intervalRunningOperationsInThreadC);

            threadSafeQueue.Push("DJP");
            Thread.Sleep(intervalRunningOperationsInThreadC);

            threadSafeQueue.Push("EKQ");
            Thread.Sleep(intervalRunningOperationsInThreadC);

            threadSafeQueue.Push("FLR");
            Thread.Sleep(intervalRunningOperationsInThreadC);

            threadSafeQueue.Push("MSX");
            Thread.Sleep(intervalRunningOperationsInThreadC);
        }

        private void ThreadDWithStringOperations(object data)
        {
            int intervalRunningOperationsInThreadD = 0;
            comboboxIntervalRunningOperationsInThreadD.Dispatcher.Invoke(new Action(() =>
            {
                intervalRunningOperationsInThreadD = (comboboxIntervalRunningOperationsInThreadD.SelectedIndex + 1) * 1000;
            }));

            ThreadSafeQueue<string> threadSafeQueue = data as ThreadSafeQueue<string>;

            threadSafeQueue.Push("NTY");
            Thread.Sleep(intervalRunningOperationsInThreadD);

            threadSafeQueue.Push("OUZ");
            Thread.Sleep(intervalRunningOperationsInThreadD);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadD);

            threadSafeQueue.Push("PVM");
            Thread.Sleep(intervalRunningOperationsInThreadD);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadD);

            threadSafeQueue.Push("QWR");
            Thread.Sleep(intervalRunningOperationsInThreadD);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadD);

            threadSafeQueue.Push("XYZ");
            Thread.Sleep(intervalRunningOperationsInThreadD);
        }

        #endregion

        #region Запуск потоков содержащих численные операции

        private void RunThreadsWithIntegerOperations()
        {
            foreach (Thread thread in Threads)
                if (thread != null && thread.IsAlive)
                    thread.Abort();

            Report report = new Report();
            report.AddOperationInReport += AddOperationInReport;

            ThreadSafeQueue<int> threadSafeQueue = new ThreadSafeQueue<int>(report);

            Thread threadA = new Thread(ThreadAWithIntegerOperations);
            threadA.Name = "Поток А";

            Thread threadB = new Thread(ThreadBWithIntegerOperations);
            threadB.Name = "Поток B";

            Thread threadC = new Thread(ThreadCWithIntegerOperations);
            threadC.Name = "Поток C";

            Thread threadD = new Thread(ThreadDWithIntegerOperations);
            threadD.Name = "Поток D";

            Threads = new Thread[] { threadA, threadB, threadC, threadD };

            if (richtextboxOperationInThreadA.IsEnabled)
                threadA.Start(threadSafeQueue);

            if (richtextboxOperationInThreadB.IsEnabled)
                threadB.Start(threadSafeQueue);

            if (richtextboxOperationInThreadC.IsEnabled)
                threadC.Start(threadSafeQueue);

            if (richtextboxOperationInThreadD.IsEnabled)
                threadD.Start(threadSafeQueue);
        }

        private void ThreadAWithIntegerOperations(object data)
        {
            int intervalRunningOperationsInThreadA = 0;
            comboboxIntervalRunningOperationsInThreadA.Dispatcher.Invoke(new Action(() =>
            {
                intervalRunningOperationsInThreadA = (comboboxIntervalRunningOperationsInThreadA.SelectedIndex + 1) * 1000;
            }));

            ThreadSafeQueue<int> threadSafeQueue = data as ThreadSafeQueue<int>;

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadA);

            threadSafeQueue.Push(-64);
            Thread.Sleep(intervalRunningOperationsInThreadA);

            threadSafeQueue.Push(378);
            Thread.Sleep(intervalRunningOperationsInThreadA);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadA);

            threadSafeQueue.Push(495);
            Thread.Sleep(intervalRunningOperationsInThreadA);

            threadSafeQueue.Push(560);
            Thread.Sleep(intervalRunningOperationsInThreadA);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadA);

            threadSafeQueue.Push(-91);
            Thread.Sleep(intervalRunningOperationsInThreadA);
        }

        private void ThreadBWithIntegerOperations(object data)
        {
            int intervalRunningOperationsInThreadB = 0;
            comboboxIntervalRunningOperationsInThreadB.Dispatcher.Invoke(new Action(() =>
            {
                intervalRunningOperationsInThreadB = (comboboxIntervalRunningOperationsInThreadB.SelectedIndex + 1) * 1000;
            }));

            ThreadSafeQueue<int> threadSafeQueue = data as ThreadSafeQueue<int>;

            threadSafeQueue.Push(132);
            Thread.Sleep(intervalRunningOperationsInThreadB);
            
            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadB);

            threadSafeQueue.Push(256);
            Thread.Sleep(intervalRunningOperationsInThreadB);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadB);

            threadSafeQueue.Push(-88);
            Thread.Sleep(intervalRunningOperationsInThreadB);

            threadSafeQueue.Push(804);
            Thread.Sleep(intervalRunningOperationsInThreadB);

            threadSafeQueue.Push(-70);
            Thread.Sleep(intervalRunningOperationsInThreadB);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadB);
        }

        private void ThreadCWithIntegerOperations(object data)
        {
            int intervalRunningOperationsInThreadC = 0;
            comboboxIntervalRunningOperationsInThreadC.Dispatcher.Invoke(new Action(() =>
            {
                intervalRunningOperationsInThreadC = (comboboxIntervalRunningOperationsInThreadC.SelectedIndex + 1) * 1000;
            }));

            ThreadSafeQueue<int> threadSafeQueue = data as ThreadSafeQueue<int>;

            threadSafeQueue.Push(725);
            Thread.Sleep(intervalRunningOperationsInThreadC);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadC);

            threadSafeQueue.Push(953);
            Thread.Sleep(intervalRunningOperationsInThreadC);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadC);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadC);

            threadSafeQueue.Push(-48);
            Thread.Sleep(intervalRunningOperationsInThreadC);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadC);

            threadSafeQueue.Push(743);
            Thread.Sleep(intervalRunningOperationsInThreadC);
        }

        private void ThreadDWithIntegerOperations(object data)
        {
            int intervalRunningOperationsInThreadD = 0;
            comboboxIntervalRunningOperationsInThreadD.Dispatcher.Invoke(new Action(() =>
            {
                intervalRunningOperationsInThreadD = (comboboxIntervalRunningOperationsInThreadD.SelectedIndex + 1) * 1000;
            }));

            ThreadSafeQueue<int> threadSafeQueue = data as ThreadSafeQueue<int>;

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadD);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadD);

            threadSafeQueue.Push(-12);
            Thread.Sleep(intervalRunningOperationsInThreadD);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadD);

            threadSafeQueue.Push(-39);
            Thread.Sleep(intervalRunningOperationsInThreadD);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadD);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadD);

            threadSafeQueue.Push(154);
            Thread.Sleep(intervalRunningOperationsInThreadD);
        }

        #endregion

        #region Запуск потоков содержащих символьные операции

        private void RunThreadsWithCharacterOperations()
        {
            foreach (Thread thread in Threads)
                if (thread != null && thread.IsAlive)
                    thread.Abort();

            Report report = new Report();
            report.AddOperationInReport += AddOperationInReport;

            ThreadSafeQueue<char> threadSafeQueue = new ThreadSafeQueue<char>(report);

            Thread threadA = new Thread(ThreadAWithCharacterOperations);
            threadA.Name = "Поток А";

            Thread threadB = new Thread(ThreadBWithCharacterOperations);
            threadB.Name = "Поток B";

            Thread threadC = new Thread(ThreadCWithCharacterOperations);
            threadC.Name = "Поток C";

            Thread threadD = new Thread(ThreadDWithCharacterOperations);
            threadD.Name = "Поток D";

            Threads = new Thread[] { threadA, threadB, threadC, threadD };

            if (richtextboxOperationInThreadA.IsEnabled)
                threadA.Start(threadSafeQueue);

            if (richtextboxOperationInThreadB.IsEnabled)
                threadB.Start(threadSafeQueue);

            if (richtextboxOperationInThreadC.IsEnabled)
                threadC.Start(threadSafeQueue);

            if (richtextboxOperationInThreadD.IsEnabled)
                threadD.Start(threadSafeQueue);
        }

        private void ThreadAWithCharacterOperations(object data)
        {
            int intervalRunningOperationsInThreadA = 0;
            comboboxIntervalRunningOperationsInThreadA.Dispatcher.Invoke(new Action(() =>
            {
                intervalRunningOperationsInThreadA = (comboboxIntervalRunningOperationsInThreadA.SelectedIndex + 1) * 1000;
            }));

            ThreadSafeQueue<char> threadSafeQueue = data as ThreadSafeQueue<char>;

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadA);

            threadSafeQueue.Push('А');
            Thread.Sleep(intervalRunningOperationsInThreadA);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadA);

            threadSafeQueue.Push('Б');
            Thread.Sleep(intervalRunningOperationsInThreadA);

            threadSafeQueue.Push('Я');
            Thread.Sleep(intervalRunningOperationsInThreadA);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadA);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadA);

            threadSafeQueue.Push('В');
            Thread.Sleep(intervalRunningOperationsInThreadA);
        }

        private void ThreadBWithCharacterOperations(object data)
        {
            int intervalRunningOperationsInThreadB = 0;
            comboboxIntervalRunningOperationsInThreadB.Dispatcher.Invoke(new Action(() =>
            {
                intervalRunningOperationsInThreadB = (comboboxIntervalRunningOperationsInThreadB.SelectedIndex + 1) * 1000;
            }));

            ThreadSafeQueue<char> threadSafeQueue = data as ThreadSafeQueue<char>;

            threadSafeQueue.Push('Г');
            Thread.Sleep(intervalRunningOperationsInThreadB);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadB);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadB);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadB);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadB);

            threadSafeQueue.Push('Д');
            Thread.Sleep(intervalRunningOperationsInThreadB);

            threadSafeQueue.Push('Е');
            Thread.Sleep(intervalRunningOperationsInThreadB);

            threadSafeQueue.Push('Ё');
            Thread.Sleep(intervalRunningOperationsInThreadB);
        }

        private void ThreadCWithCharacterOperations(object data)
        {
            int intervalRunningOperationsInThreadC = 0;
            comboboxIntervalRunningOperationsInThreadC.Dispatcher.Invoke(new Action(() =>
            {
                intervalRunningOperationsInThreadC = (comboboxIntervalRunningOperationsInThreadC.SelectedIndex + 1) * 1000;
            }));

            ThreadSafeQueue<char> threadSafeQueue = data as ThreadSafeQueue<char>;

            threadSafeQueue.Push('Ж');
            Thread.Sleep(intervalRunningOperationsInThreadC);

            threadSafeQueue.Push('З');
            Thread.Sleep(intervalRunningOperationsInThreadC);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadC);

            threadSafeQueue.Push('И');
            Thread.Sleep(intervalRunningOperationsInThreadC);

            threadSafeQueue.Push('Й');
            Thread.Sleep(intervalRunningOperationsInThreadC);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadC);

            threadSafeQueue.Push('К');
            Thread.Sleep(intervalRunningOperationsInThreadC);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadC);
        }

        private void ThreadDWithCharacterOperations(object data)
        {
            int intervalRunningOperationsInThreadD = 0;
            comboboxIntervalRunningOperationsInThreadD.Dispatcher.Invoke(new Action(() =>
            {
                intervalRunningOperationsInThreadD = (comboboxIntervalRunningOperationsInThreadD.SelectedIndex + 1) * 1000;
            }));

            ThreadSafeQueue<char> threadSafeQueue = data as ThreadSafeQueue<char>;

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadD);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadD);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadD);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadD);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadD);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadD);

            threadSafeQueue.Pop();
            Thread.Sleep(intervalRunningOperationsInThreadD);

            threadSafeQueue.Push('Л');
            Thread.Sleep(intervalRunningOperationsInThreadD);
        }

        #endregion

        #region Загрузка тестовых операций в потоки

        private void LoadStringOperationsInThreads(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            EnableAndCleanAllThreads();

            richtextboxOperationInThreadA.AppendText("string Pop()"  + "\n\n");
            richtextboxOperationInThreadA.AppendText("Push(\"ABC\")" + "\n");
            richtextboxOperationInThreadA.AppendText("Push(\"DEF\")" + "\n");
            richtextboxOperationInThreadA.AppendText("Push(\"GHI\")" + "\n");
            richtextboxOperationInThreadA.AppendText("Push(\"JKL\")" + "\n");
            richtextboxOperationInThreadA.AppendText("string Pop()"  + "\n");
            richtextboxOperationInThreadA.AppendText("string Pop()"  + "\n");
            richtextboxOperationInThreadA.AppendText("Push(\"MNO\")");
            comboboxIntervalRunningOperationsInThreadA.SelectedIndex = 0;

            richtextboxOperationInThreadB.AppendText("Push(\"PQR\")" + "\n\n");
            richtextboxOperationInThreadB.AppendText("Push(\"STU\")" + "\n");
            richtextboxOperationInThreadB.AppendText("Push(\"VWX\")" + "\n");
            richtextboxOperationInThreadB.AppendText("string Pop()"  + "\n");
            richtextboxOperationInThreadB.AppendText("Push(\"YZA\")" + "\n");
            richtextboxOperationInThreadB.AppendText("string Pop()"  + "\n");
            richtextboxOperationInThreadB.AppendText("Push(\"AGM\")" + "\n");
            richtextboxOperationInThreadB.AppendText("Push(\"BHN\")");
            comboboxIntervalRunningOperationsInThreadB.SelectedIndex = 1;

            richtextboxOperationInThreadC.AppendText("string Pop()"  + "\n\n");
            richtextboxOperationInThreadC.AppendText("string Pop()"  + "\n");
            richtextboxOperationInThreadC.AppendText("string Pop()"  + "\n");
            richtextboxOperationInThreadC.AppendText("Push(\"CIO\")" + "\n");
            richtextboxOperationInThreadC.AppendText("Push(\"DJP\")" + "\n");
            richtextboxOperationInThreadC.AppendText("Push(\"EKQ\")" + "\n");
            richtextboxOperationInThreadC.AppendText("Push(\"FLR\")" + "\n");
            richtextboxOperationInThreadC.AppendText("Push(\"MSX\")");
            comboboxIntervalRunningOperationsInThreadC.SelectedIndex = 1;

            richtextboxOperationInThreadD.AppendText("Push(\"NTY\")" + "\n\n");
            richtextboxOperationInThreadD.AppendText("Push(\"OUZ\")" + "\n");
            richtextboxOperationInThreadD.AppendText("string Pop()"  + "\n");
            richtextboxOperationInThreadD.AppendText("Push(\"PVW\")" + "\n");
            richtextboxOperationInThreadD.AppendText("string Pop()"  + "\n");
            richtextboxOperationInThreadD.AppendText("Push(\"QWR\")" + "\n");
            richtextboxOperationInThreadD.AppendText("string Pop()"  + "\n");
            richtextboxOperationInThreadD.AppendText("Push(\"XYZ\")");
            comboboxIntervalRunningOperationsInThreadD.SelectedIndex = 0;

            FormattingDefinedOperationsInThreads();
            ThreadsStartupOption = ThreadsStartupOption.String;
            Cursor = Cursors.Arrow;
        }

        private void LoadIntegerOperationsInThreads(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            EnableAndCleanAllThreads();

            richtextboxOperationInThreadA.AppendText("int Pop()" + "\n\n");
            richtextboxOperationInThreadA.AppendText("Push(-64)"   + "\n");
            richtextboxOperationInThreadA.AppendText("Push(378)"   + "\n");
            richtextboxOperationInThreadA.AppendText("int Pop()" + "\n");
            richtextboxOperationInThreadA.AppendText("Push(495)"   + "\n");
            richtextboxOperationInThreadA.AppendText("Push(560)"   + "\n");
            richtextboxOperationInThreadA.AppendText("int Pop()" + "\n");
            richtextboxOperationInThreadA.AppendText("Push(-91)");
            comboboxIntervalRunningOperationsInThreadA.SelectedIndex = 0;

            richtextboxOperationInThreadB.AppendText("Push(132)"   + "\n\n");
            richtextboxOperationInThreadB.AppendText("int Pop()" + "\n");
            richtextboxOperationInThreadB.AppendText("Push(256)"   + "\n");
            richtextboxOperationInThreadB.AppendText("int Pop()" + "\n");
            richtextboxOperationInThreadB.AppendText("Push(-88)"   + "\n");
            richtextboxOperationInThreadB.AppendText("Push(804)"   + "\n");
            richtextboxOperationInThreadB.AppendText("Push(-70)"   + "\n");
            richtextboxOperationInThreadB.AppendText("int Pop()");
            comboboxIntervalRunningOperationsInThreadB.SelectedIndex = 1;

            richtextboxOperationInThreadC.AppendText("Push(725)"   + "\n\n");
            richtextboxOperationInThreadC.AppendText("int Pop()" + "\n");
            richtextboxOperationInThreadC.AppendText("Push(953)"   + "\n");
            richtextboxOperationInThreadC.AppendText("int Pop()" + "\n");
            richtextboxOperationInThreadC.AppendText("int Pop()" + "\n");
            richtextboxOperationInThreadC.AppendText("Push(-48)"   + "\n");
            richtextboxOperationInThreadC.AppendText("int Pop()" + "\n");
            richtextboxOperationInThreadC.AppendText("Push(743)");
            comboboxIntervalRunningOperationsInThreadC.SelectedIndex = 2;

            richtextboxOperationInThreadD.AppendText("int Pop()" + "\n\n");
            richtextboxOperationInThreadD.AppendText("int Pop()" + "\n");
            richtextboxOperationInThreadD.AppendText("Push(-12)"   + "\n");
            richtextboxOperationInThreadD.AppendText("int Pop()" + "\n");
            richtextboxOperationInThreadD.AppendText("Push(-39)"   + "\n");
            richtextboxOperationInThreadD.AppendText("int Pop()" + "\n");
            richtextboxOperationInThreadD.AppendText("int Pop()" + "\n");
            richtextboxOperationInThreadD.AppendText("Push(154)");
            comboboxIntervalRunningOperationsInThreadD.SelectedIndex = 0;

            FormattingDefinedOperationsInThreads();
            ThreadsStartupOption = ThreadsStartupOption.Integer;
            Cursor = Cursors.Arrow;
        }

        private void LoadCharacterOperationsInThreads(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            EnableAndCleanAllThreads();

            richtextboxOperationInThreadA.AppendText("char Pop()" + "\n\n");
            richtextboxOperationInThreadA.AppendText("Push('А')" + "\n");
            richtextboxOperationInThreadA.AppendText("char Pop()" + "\n");
            richtextboxOperationInThreadA.AppendText("Push('Б')" + "\n");
            richtextboxOperationInThreadA.AppendText("Push('Я')" + "\n");
            richtextboxOperationInThreadA.AppendText("char Pop()" + "\n");
            richtextboxOperationInThreadA.AppendText("char Pop()" + "\n");
            richtextboxOperationInThreadA.AppendText("Push('В')");
            comboboxIntervalRunningOperationsInThreadA.SelectedIndex = 4;

            richtextboxOperationInThreadB.AppendText("Push('Г')" + "\n\n");
            richtextboxOperationInThreadB.AppendText("char Pop()" + "\n");
            richtextboxOperationInThreadB.AppendText("char Pop()" + "\n");
            richtextboxOperationInThreadB.AppendText("char Pop()" + "\n");
            richtextboxOperationInThreadB.AppendText("char Pop()" + "\n");
            richtextboxOperationInThreadB.AppendText("Push('Д')" + "\n");
            richtextboxOperationInThreadB.AppendText("Push('Е')" + "\n");
            richtextboxOperationInThreadB.AppendText("Push('Ё')");
            comboboxIntervalRunningOperationsInThreadB.SelectedIndex = 2;
        
            richtextboxOperationInThreadC.AppendText("Push('Ж')" + "\n\n");
            richtextboxOperationInThreadC.AppendText("Push('З')" + "\n");
            richtextboxOperationInThreadC.AppendText("char Pop()" + "\n");
            richtextboxOperationInThreadC.AppendText("Push('И')" + "\n");
            richtextboxOperationInThreadC.AppendText("Push('Й')" + "\n");
            richtextboxOperationInThreadC.AppendText("char Pop()" + "\n");
            richtextboxOperationInThreadC.AppendText("Push('К')" + "\n");
            richtextboxOperationInThreadC.AppendText("char Pop()");
            comboboxIntervalRunningOperationsInThreadC.SelectedIndex = 4;

            richtextboxOperationInThreadD.AppendText("char Pop()" + "\n\n");
            richtextboxOperationInThreadD.AppendText("char Pop()" + "\n");
            richtextboxOperationInThreadD.AppendText("char Pop()" + "\n");
            richtextboxOperationInThreadD.AppendText("char Pop()" + "\n");
            richtextboxOperationInThreadD.AppendText("char Pop()" + "\n");
            richtextboxOperationInThreadD.AppendText("char Pop()" + "\n");
            richtextboxOperationInThreadD.AppendText("char Pop()" + "\n");
            richtextboxOperationInThreadD.AppendText("Push('Л')");
            comboboxIntervalRunningOperationsInThreadD.SelectedIndex = 1;

            FormattingDefinedOperationsInThreads();
            ThreadsStartupOption = ThreadsStartupOption.Char;
            Cursor = Cursors.Arrow;
        }

        private void FormattingDefinedOperations(RichTextBox richTextBox)
        {
            foreach (Paragraph paragraph in richTextBox.Document.Blocks)
                if (new TextRange(paragraph.ContentStart, paragraph.ContentEnd).Text.Contains("Push"))
                    paragraph.Foreground = Brushes.Green;
                else
                    paragraph.Foreground = Brushes.Blue;
        }

        private void FormattingDefinedOperationsInThreads()
        {
            FormattingDefinedOperations(richtextboxOperationInThreadA);
            FormattingDefinedOperations(richtextboxOperationInThreadB);
            FormattingDefinedOperations(richtextboxOperationInThreadC);
            FormattingDefinedOperations(richtextboxOperationInThreadD);
        }

        #endregion

        #region Завершение работы приложения

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (Thread thread in Threads)
                if (thread != null && thread.IsAlive)
                    thread.Abort();
        }

        #endregion
    }
}
