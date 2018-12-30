using System.Windows;

namespace HomeWork.App
{
    /// <inheritdoc cref="System.Windows.Window" />
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow() => InitializeComponent();

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            const string webServerAddress = "https://localhost:44382/";
            ChromiumContainer.Content = new CefSharp.Wpf.ChromiumWebBrowser()
            {
                Address = webServerAddress
            };
        }
        
    }
}
