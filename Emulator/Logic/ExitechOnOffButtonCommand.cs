using System;
using System.Windows;
using Emulator.Logic.Exitech;
using Emulator.ViewModel;

namespace Emulator.Logic
{
    public class ExitechOnOffButtonCommand: CommandWithDelay
    {
        public ExitechStateMachine ExitechStateMachine { get; set; }

        protected override void TimerHandler(object state)
        {
            base.TimerHandler(state);
            if (Counter == 4)
            {
                ExitechStateMachine.Fire(ExitechDeviceTriggers.Clear);
            }
            else
            {
                ExitechStateMachine.Fire(ExitechDeviceTriggers.OnOffTimerTick);
            }
        }

        public override void DoDoubleClickExecute()
        {
            base.DoDoubleClickExecute();
            ExitechStateMachine.Fire(ExitechDeviceTriggers.OnOffButtonDoubleClick);
        }

        public override void DoClickExecute()
        {
            base.DoClickExecute();
            ExitechStateMachine.Fire(ExitechDeviceTriggers.OnOffButtonClick);
        }
    }
}