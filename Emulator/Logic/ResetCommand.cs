using System;
using System.Windows.Input;
using Common.Logging;
using Emulator.Common.Interfaces;
using Emulator.ViewModel;

namespace Emulator.Logic
{
    /// <summary>
    /// Команда сброса данных до исходных
    /// </summary>
    public class ResetCommand: ICommand
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ResetCommand));
        public MainViewModel ViewModel { get; set; }
        public IDataProvider DataProvider { get; set; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            DataProvider.Reset(ViewModel.Variant);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}