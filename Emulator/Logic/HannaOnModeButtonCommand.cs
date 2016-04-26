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

        protected override void TimerHandler(object state)
        {
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