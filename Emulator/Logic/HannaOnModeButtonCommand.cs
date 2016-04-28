using Emulator.Logic.Hanna;

namespace Emulator.Logic
{
    public class HannaOnModeButtonCommand: CommandWithDelay
    {
        public HannaStateMachine HannaStateMachine { get; set; }
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void DoReleaseExecute()
        {
            base.DoReleaseExecute();
            HannaStateMachine.Fire(HannaDeviceTriggers.OnModeButtonRelease);
        }

        protected override void TimerHandler(object state)
        {
            base.TimerHandler(state);
            HannaStateMachine.Fire(HannaDeviceTriggers.OnModeButtonLongClick);
        }

        public override void DoClickExecute()
        {
            base.DoClickExecute();
            HannaStateMachine.Fire(HannaDeviceTriggers.OnModeButtonClick);
        }

        public override void DoDoubleClickExecute()
        {
            base.DoDoubleClickExecute();
            HannaStateMachine.Fire(HannaDeviceTriggers.OnModeButtonDoubleClick);
        }
    }
}