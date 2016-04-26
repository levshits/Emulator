using System;
using Emulator.ViewModel;

namespace Emulator.Logic
{
    public class ExitecCallRecallButtonCommand: CommandWithDelay
    {
        public ExitecDeviceViewModel ExitecDeviceViewModel { get; set; }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void DoReleaseExecute()
        {
            base.DoReleaseExecute();
            Log.Debug("Timer release");
            Timer.Change(-1, -1);
            ExitecDeviceViewModel.LittleScreenText = "Rel";
        }

        protected override void TimerHandler(object state)
        {
            Log.Debug("Timer tick");
            Counter++;
            ExitecDeviceViewModel.BigScreenText = Counter.ToString();
        }
    }
}