using System;
using System.Windows.Input;

namespace Emulator.Logic
{
    public abstract class CommandWithDelay : ICommand
    {
        public int DelayInMiliseconds { get; set; }
        public abstract bool CanExecute(object parameter);

        public virtual void Execute(object parameter)
        {
            
            
        }
        public abstract void DoExecute(object parameter);
        public event EventHandler CanExecuteChanged;
    }
}