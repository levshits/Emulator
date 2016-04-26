using System;
using System.Threading;
using Emulator.Logic.Exitech;
using Emulator.ViewModel;

namespace Emulator.Logic
{
    public class ExitechModeHoldButtonCommand: CommandWithDelay
    {
        public ExitechStateMachine ExitechStateMachine { get; set; }

        protected override void TimerHandler(object state)
        {
            base.TimerHandler(state);
            ExitechStateMachine.Fire(ExitechDeviceTriggers.ModeHoldTimerTick);
        }

        public override void DoDoubleClickExecute()
        {
            base.DoDoubleClickExecute();
            ExitechStateMachine.Fire(ExitechDeviceTriggers.ModeHoldDoubleClick);
        }

        public override void DoClickExecute()
        {
            base.DoClickExecute();
            ExitechStateMachine.Fire(ExitechDeviceTriggers.ModeHoldClick);
        }

        public override void DoReleaseExecute()
        {
            base.DoReleaseExecute();
            ExitechStateMachine.Fire(ExitechDeviceTriggers.ModeHoldRelease);
        }
    }
}