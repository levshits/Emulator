using System;
using System.Windows.Input;
using Common.Logging;

namespace Emulator.Logic
{
    public class ResetCommand: ICommand
    {
        private static ILog _log = LogManager.GetLogger(typeof(ResetCommand));
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _log.Debug("Reset");
        }

        public event EventHandler CanExecuteChanged;
    }
}