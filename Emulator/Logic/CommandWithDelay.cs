using System;
using System.Threading;
using System.Windows.Input;
using Common.Logging;

namespace Emulator.Logic
{
    public abstract class CommandWithDelay : ICommand
    {
        protected static readonly ILog Log = LogManager.GetLogger<CommandWithDelay>();
        public abstract bool CanExecute(object parameter);
        public Timer Timer { get; set; }
        public int Counter { get; set; }

        protected CommandWithDelay()
        {
            Timer = new Timer(TimerHandler);
        }

        protected abstract void TimerHandler(object state);

        public virtual void Execute(object parameter)
        {
            MouseButtonEventArgs args = parameter as MouseButtonEventArgs;
            if (args != null)
            {
                if (args.ButtonState == MouseButtonState.Pressed)
                {
                    DoPressExecute();
                }
                else
                {
                    DoReleaseExecute();
                }
            }
        }

        /// <summary>
        /// Does the execute.
        /// Please, implement timer  logic here
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public virtual void DoPressExecute()
        {
            Counter = 0;
        }

        public virtual void DoReleaseExecute()
        {
            Log.Debug("Timer release");
            Timer.Change(-1, -1);
            if (Counter == 0)
            {
                DoShortClickExecute();
            }
        }

        public virtual void DoShortClickExecute()
        {
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}