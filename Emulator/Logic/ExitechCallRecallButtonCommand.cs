using System;
using Emulator.Logic.Exitech;
using Emulator.ViewModel;

namespace Emulator.Logic
{
    public class ExitechCallRecallButtonCommand: CommandWithDelay
    {
        public ExitechStateMachine ExitechStateMachine { get; set; }


        protected override void TimerHandler(object state)
        {
            base.TimerHandler(state);
            ExitechStateMachine.Fire(ExitechDeviceTriggers.CallRecallTimerTick);
        }

        public override void DoDoubleClickExecute()
        {
            base.DoDoubleClickExecute();
            ExitechStateMachine.Fire(ExitechDeviceTriggers.CallRecallDoubleClick);
        }

        public override void DoClickExecute()
        {
            base.DoClickExecute();
            ExitechStateMachine.Fire(ExitechDeviceTriggers.CallRecallClick);
        }
    }
}