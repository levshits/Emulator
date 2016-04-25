using System;
using System.Threading;
using System.Windows.Input;
using Common.Logging;
using Emulator.Model;

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
            Guid? args = parameter as Guid?;

            if (args.HasValue)
            {
                if (args == DelayedCommandEvents.MouseDown)
                {
                    DoPressExecute();
                }
                else if (args == DelayedCommandEvents.MouseUp)
                {
                    DoReleaseExecute();
                }
                else if (args == DelayedCommandEvents.Click && Counter == 0)
                {
                    DoClickExecute();
                }
                else if (args == DelayedCommandEvents.DoubleClick && Counter == 0)
                {
                    DoDoubleClickExecute();
                }
            }
        }

        public virtual void DoDoubleClickExecute()
        {
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
        }

        public virtual void DoClickExecute()
        {
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}