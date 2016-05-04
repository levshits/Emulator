using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Emulator.Common.Interfaces;
using Emulator.Common.Models;
using Emulator.View;
using GalaSoft.MvvmLight.Command;
using MaterialDesignThemes.Wpf;

namespace Emulator.Logic
{
    /// <summary>
    /// Команда для кнопки применения фильтра
    /// </summary>
    public class ApplyFilterCommand : ICommand
    {
        public IDataProvider DataProvider { get; set; }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            var view = new FilterDialog
            {
                DataContext = new {Command = new RelayCommand( async () =>
                {
                    await Task.Delay(TimeSpan.FromSeconds(3));
                    DialogHost.CloseDialogCommand.Execute(null, null);
                    DataProvider.ApplyFilter((FilterModel)parameter);
                })}
            };
            await DialogHost.Show(view, "RootDialog");


        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}