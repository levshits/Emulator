using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Emulator.Views;
using Spring.Context.Support;

namespace Emulator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var ctx = ContextRegistry.GetContext();
            var model = ctx.GetObject("MainViewModel");
            var view = new MainWindow() { DataContext = model };
            view.Show();
        }
    }
}
