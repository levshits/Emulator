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
            throw new System.NotImplementedException();
        }
    }
}