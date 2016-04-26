using System;
using System.Windows;
using System.Windows.Threading;
using Common.Logging;
using Emulator.View;
using Spring.Context.Support;

namespace Emulator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof (App));
        protected override void OnStartup(StartupEventArgs e)
        {
            var ctx = ContextRegistry.GetContext();
            var model = ctx.GetObject("MainViewModel");
            var view = new MainWindow() { DataContext = model };
            view.Show();
        }

        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            _log.Fatal("Some fatal error occured", e.Exception);
        }
    }
}
