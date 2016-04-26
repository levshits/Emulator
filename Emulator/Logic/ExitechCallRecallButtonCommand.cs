using System;
using Emulator.ViewModel;

namespace Emulator.Logic
{
    public class ExitechCallRecallButtonCommand: CommandWithDelay
    {
        public ExitechDeviceViewModel ExitechDeviceViewModel { get; set; }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void DoReleaseExecute()
        {
            base.DoReleaseExecute();
            Log.Debug("Timer release");
            Timer.Change(-1, -1);
            ExitechDeviceViewModel.LittleScreenText = "Rel";
        }

        protected override void TimerHandler(object state)
        {
            Log.Debug("Timer tick");
            Counter++;
            ExitechDeviceViewModel.BigScreenText = Counter.ToString();
        }
    }
}