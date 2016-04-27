using Emulator.Logic.Hanna;

namespace Emulator.Logic
{
    public class HannaSetHoldButtonCommand: CommandWithDelay
    {
        public HannaStateMachine HannaStateMachine { get; set; }
        protected override void TimerHandler(object state)
        {
            base.TimerHandler(state);
            HannaStateMachine.Fire(HannaDeviceTriggers.SetHoldButtonLongClick);
        }

        public override void DoClickExecute()
        {
            base.DoClickExecute();
            HannaStateMachine.Fire(HannaDeviceTriggers.SetHoldButtonClick);
        }

        public override void DoReleaseExecute()
        {
            base.DoReleaseExecute();
            HannaStateMachine.Fire(HannaDeviceTriggers.SetHoldButtonRelease);
        }

        public override void DoDoubleClickExecute()
        {
            base.DoDoubleClickExecute();
            HannaStateMachine.Fire(HannaDeviceTriggers.SetHoldButtonDoubleClick);
        }
    }
}