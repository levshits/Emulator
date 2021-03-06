﻿using System;
using System.Threading;
using System.Windows.Input;
using Common.Logging;
using Emulator.Model;

namespace Emulator.Logic
{
    /// <summary>
    /// Базовй класс для всех комманд кнопок прибороы
    /// </summary>
    public abstract class CommandWithDelay : ICommand
    {
        protected static readonly ILog Log = LogManager.GetLogger<CommandWithDelay>();
        public Timer Timer { get; set; }
        public int Counter { get; set; }

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        protected CommandWithDelay()
        {
            Timer = new Timer(TimerHandler);
        }

        protected virtual void TimerHandler(object state)
        {
            Counter++;
        }

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
            Timer.Change(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
        }

        public virtual void DoReleaseExecute()
        {
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