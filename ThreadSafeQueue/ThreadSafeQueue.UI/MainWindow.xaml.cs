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

        private void RunThreads(object sender, RoutedEventArgs e)
        {
            if (!labelHeaderThreadA.IsEnabled && !labelHeaderThreadB.IsEnabled && !labelHeaderThreadC.IsEnabled && !labelHeaderThreadD.IsEnabled)
                MessageBox.Show("Для успешного запуска потоков необходимо выполнить включение в работу как минимум одного из потоков.", "Ошибка при запуске потоков", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {

            }
        }

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
            Cursor = Cursors.Arrow;
        }

        private void LoadIntegerOperationsInThreads(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            EnableAndCleanAllThreads();

            richtextboxOperationInThreadA.AppendText("Int32 Pop()" + "\n\n");
            richtextboxOperationInThreadA.AppendText("Push(-64)"   + "\n");
            richtextboxOperationInThreadA.AppendText("Push(378)"   + "\n");
            richtextboxOperationInThreadA.AppendText("Int32 Pop()" + "\n");
            richtextboxOperationInThreadA.AppendText("Push(495)"   + "\n");
            richtextboxOperationInThreadA.AppendText("Push(560)"   + "\n");
            richtextboxOperationInThreadA.AppendText("Int32 Pop()" + "\n");
            richtextboxOperationInThreadA.AppendText("Push(-91)");
            comboboxIntervalRunningOperationsInThreadA.SelectedIndex = 0;

            richtextboxOperationInThreadB.AppendText("Push(132)"   + "\n\n");
            richtextboxOperationInThreadB.AppendText("Int32 Pop()" + "\n");
            richtextboxOperationInThreadB.AppendText("Push(256)"   + "\n");
            richtextboxOperationInThreadB.AppendText("Int32 Pop()" + "\n");
            richtextboxOperationInThreadB.AppendText("Push(-88)"   + "\n");
            richtextboxOperationInThreadB.AppendText("Push(804)"   + "\n");
            richtextboxOperationInThreadB.AppendText("Push(-70)"   + "\n");
            richtextboxOperationInThreadB.AppendText("Int32 Pop()");
            comboboxIntervalRunningOperationsInThreadB.SelectedIndex = 1;

            richtextboxOperationInThreadC.AppendText("Push(725)"   + "\n\n");
            richtextboxOperationInThreadC.AppendText("Int32 Pop()" + "\n");
            richtextboxOperationInThreadC.AppendText("Push(953)"   + "\n");
            richtextboxOperationInThreadC.AppendText("Int32 Pop()" + "\n");
            richtextboxOperationInThreadC.AppendText("Int32 Pop()" + "\n");
            richtextboxOperationInThreadC.AppendText("Push(-48)"   + "\n");
            richtextboxOperationInThreadC.AppendText("Int32 Pop()" + "\n");
            richtextboxOperationInThreadC.AppendText("Push(743)");
            comboboxIntervalRunningOperationsInThreadC.SelectedIndex = 2;

            richtextboxOperationInThreadD.AppendText("Int32 Pop()" + "\n\n");
            richtextboxOperationInThreadD.AppendText("Int32 Pop()" + "\n");
            richtextboxOperationInThreadD.AppendText("Push(-12)"   + "\n");
            richtextboxOperationInThreadD.AppendText("Int32 Pop()" + "\n");
            richtextboxOperationInThreadD.AppendText("Push(-39)"   + "\n");
            richtextboxOperationInThreadD.AppendText("Int32 Pop()" + "\n");
            richtextboxOperationInThreadD.AppendText("Int32 Pop()" + "\n");
            richtextboxOperationInThreadD.AppendText("Push(154)");
            comboboxIntervalRunningOperationsInThreadD.SelectedIndex = 0;

            FormattingDefinedOperationsInThreads();
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
            richtextboxOperationInThreadA.AppendText("Push('')" + "\n");
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
            Cursor = Cursors.Arrow;
        }
    }
}
