using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ThreadSafeQueue.UI
{
    /// <summary>
    /// Форма приложения
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Конструктор по умолчанию формы приложения
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SwitchingOnAndOffStreams(object sender, RoutedEventArgs e)
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

        private void RunThreads(object sender, RoutedEventArgs e)
        {
            if (!labelHeaderThreadA.IsEnabled && !labelHeaderThreadB.IsEnabled && !labelHeaderThreadC.IsEnabled && !labelHeaderThreadD.IsEnabled)
                MessageBox.Show("Для успешного запуска потоков, необходимо выполнить включение в работу хотя-бы одного из потоков.", "Ошибка при запуске потоков", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {

            }
        }

        private void EnableAllThreads()
        {
            labelHeaderThreadA.IsEnabled = true;
            richtextboxOperationInThreadA.IsEnabled = true;
            labelIntervalRunningOperationsInThreadA.IsEnabled = true;
            comboboxIntervalRunningOperationsInThreadA.IsEnabled = true;
            comboboxIntervalRunningOperationsInThreadA.SelectedIndex = -1;
            buttonSwitchingOnAndOffThreadA.Content = "Включен";
            buttonSwitchingOnAndOffThreadA.Foreground = Brushes.Black;

            labelHeaderThreadB.IsEnabled = true;
            richtextboxOperationInThreadB.IsEnabled = true;
            labelIntervalRunningOperationsInThreadB.IsEnabled = true;
            comboboxIntervalRunningOperationsInThreadB.IsEnabled = true;
            comboboxIntervalRunningOperationsInThreadB.SelectedIndex = -1;
            buttonSwitchingOnAndOffThreadB.Content = "Включен";
            buttonSwitchingOnAndOffThreadB.Foreground = Brushes.Black;

            labelHeaderThreadC.IsEnabled = true;
            richtextboxOperationInThreadC.IsEnabled = true;
            labelIntervalRunningOperationsInThreadC.IsEnabled = true;
            comboboxIntervalRunningOperationsInThreadC.IsEnabled = true;
            comboboxIntervalRunningOperationsInThreadC.SelectedIndex = -1;
            buttonSwitchingOnAndOffThreadC.Content = "Включен";
            buttonSwitchingOnAndOffThreadC.Foreground = Brushes.Black;

            labelHeaderThreadD.IsEnabled = true;
            richtextboxOperationInThreadD.IsEnabled = true;
            labelIntervalRunningOperationsInThreadD.IsEnabled = true;
            comboboxIntervalRunningOperationsInThreadD.IsEnabled = true;
            comboboxIntervalRunningOperationsInThreadD.SelectedIndex = -1;
            buttonSwitchingOnAndOffThreadD.Content = "Включен";
            buttonSwitchingOnAndOffThreadD.Foreground = Brushes.Black;
        }

        private void CleaningOperationsInThreads()
        {
            //richtextboxOperationInThreadA.Document
        }

        private void FormattingDefinedOperations()
        {
            FlowDocument flowDocument = richtextboxOperationInThreadA.Document;
            foreach (Paragraph paragraph in richtextboxOperationInThreadA.Document.Blocks)
                if (new TextRange(paragraph.ContentStart, paragraph.ContentEnd).Text.Contains("Push"))
                    paragraph.Foreground = Brushes.Green;
                else
                    paragraph.Foreground = Brushes.Blue;

            flowDocument = richtextboxOperationInThreadB.Document;
            foreach (Paragraph paragraph in richtextboxOperationInThreadB.Document.Blocks)
                if (new TextRange(paragraph.ContentStart, paragraph.ContentEnd).Text.Contains("Push"))
                    paragraph.Foreground = Brushes.Green;
                else
                    paragraph.Foreground = Brushes.Blue;

            flowDocument = richtextboxOperationInThreadC.Document;
            foreach (Paragraph paragraph in richtextboxOperationInThreadC.Document.Blocks)
                if (new TextRange(paragraph.ContentStart, paragraph.ContentEnd).Text.Contains("Push"))
                    paragraph.Foreground = Brushes.Green;
                else
                    paragraph.Foreground = Brushes.Blue;

            flowDocument = richtextboxOperationInThreadD.Document;
            foreach (Paragraph paragraph in richtextboxOperationInThreadD.Document.Blocks)
                if (new TextRange(paragraph.ContentStart, paragraph.ContentEnd).Text.Contains("Push"))
                    paragraph.Foreground = Brushes.Green;
                else
                    paragraph.Foreground = Brushes.Blue;
        }

        private void LoadStringOperations(object sender, RoutedEventArgs e)
        {
        }

        private void LoadIntegerOperations(object sender, RoutedEventArgs e)
        {
            EnableAllThreads();

            richtextboxOperationInThreadA.AppendText("Int32 Pop()" + "\n");
            richtextboxOperationInThreadA.AppendText("Push(-64)"   + "\n");
            richtextboxOperationInThreadA.AppendText("Push(378)"   + "\n");
            richtextboxOperationInThreadA.AppendText("Int32 Pop()" + "\n");
            richtextboxOperationInThreadA.AppendText("Push(495)"   + "\n");
            comboboxIntervalRunningOperationsInThreadA.SelectedIndex = 0;

            richtextboxOperationInThreadB.AppendText("Push(132)"   + "\n");
            richtextboxOperationInThreadB.AppendText("Int32 Pop()" + "\n");
            richtextboxOperationInThreadB.AppendText("Push(256)"   + "\n");
            richtextboxOperationInThreadB.AppendText("Int32 Pop()" + "\n");
            richtextboxOperationInThreadB.AppendText("Push(-98)"   + "\n");
            comboboxIntervalRunningOperationsInThreadB.SelectedIndex = 1;

            richtextboxOperationInThreadC.AppendText("Push(725)"   + "\n");
            richtextboxOperationInThreadC.AppendText("Int32 Pop()" + "\n");
            richtextboxOperationInThreadC.AppendText("Push(953)"   + "\n");
            richtextboxOperationInThreadC.AppendText("Int32 Pop()" + "\n");
            richtextboxOperationInThreadC.AppendText("Int32 Pop()" + "\n");
            comboboxIntervalRunningOperationsInThreadC.SelectedIndex = 2;

            richtextboxOperationInThreadD.AppendText("Int32 Pop()" + "\n");
            richtextboxOperationInThreadD.AppendText("Int32 Pop()" + "\n");
            richtextboxOperationInThreadD.AppendText("Push(-12)"   + "\n");
            richtextboxOperationInThreadD.AppendText("Int32 Pop()" + "\n");
            richtextboxOperationInThreadD.AppendText("Push(-39)"   + "\n");
            comboboxIntervalRunningOperationsInThreadD.SelectedIndex = 0;

            FormattingDefinedOperations();
        }

        private void LoadCharacterOperations(object sender, RoutedEventArgs e)
        {
        }
    }
}
