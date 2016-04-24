using System;
using System.Windows.Input;
using Common.Logging;
using Emulator.Common.Interfaces;

namespace Emulator.Logic
{
    public class ResetCommand: ICommand
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ResetCommand));
        public IDataProvider DataProvider { get; set; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Log.Debug("Reset");
            DataProvider.Reset();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}