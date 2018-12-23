using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        private static readonly IEnumerable<Process> DotnetRunProcesses =
            new[]
            {
                new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "dotnet",
                        Arguments = $"{Environment.CurrentDirectory}\\website\\Web\\HomeWork.Web.dll",
                        UseShellExecute = true,
                        RedirectStandardOutput = false,
                        RedirectStandardError = false,
                        CreateNoWindow = true,
                        WorkingDirectory = $"{Environment.CurrentDirectory}\\website\\Web\\HomeWork.Web.dll"
                    }

                },
                new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "dotnet",
                        Arguments = $"{Environment.CurrentDirectory}\\website\\Api\\HomeWork.Api.dll",
                        UseShellExecute = true,
                        RedirectStandardOutput = false,
                        RedirectStandardError = false,
                        CreateNoWindow = true,
                        WorkingDirectory = $"{Environment.CurrentDirectory}\\website\\Api\\HomeWork.Api.dll"
                    }

                }
            };

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            DotnetRunProcesses.ToList().ForEach(x => x.Start());
            const string webServerAddress = "http://localhost:44382/";
            ChromiumContainer.Content = new CefSharp.Wpf.ChromiumWebBrowser()
            {
                Address = webServerAddress
            };
            Closed += Window_Closed;
        }

        private static void Window_Closed(object sender, EventArgs e)
        {
            DotnetRunProcesses.ToList().ForEach(process =>
            {
                if (!process.HasExited) process.Kill();
            });
        }
    }
}
