using System.Windows;

namespace ThreadSafeQueue.UI
{
    /// <summary>
    /// Класс приложения Windows Presentation Foundation
    /// </summary>
    public partial class App : Application
    {
        private void ApplicationDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Необработанное исключение приложения", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}
