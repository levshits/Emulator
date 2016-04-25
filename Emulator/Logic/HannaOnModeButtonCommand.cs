namespace Emulator.Logic
{
    public class HannaOnModeButtonCommand: CommandWithDelay
    {
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