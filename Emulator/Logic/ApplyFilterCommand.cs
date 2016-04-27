using System;
using System.Windows.Input;
using Emulator.Common.Interfaces;
using Emulator.Common.Models;

namespace Emulator.Logic
{
    public class ApplyFilterCommand : ICommand
    {
        public IDataProvider DataProvider { get; set; }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            DataProvider.ApplyFilter((FilterModel) parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}