using System;
using System.Threading;
using Emulator.ViewModel;

namespace Emulator.Logic
{
    public class ExitechModeHoldButtonCommand: CommandWithDelay
    {
        public ExitechDeviceViewModel ExitechDeviceViewModel { get; set; }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void DoPressExecute()
        {
            base.DoPressExecute();
            Log.Debug("Execute press");
            Timer.Change(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
        }

        public override void DoReleaseExecute()
        {
            base.DoReleaseExecute();
            ExitechDeviceViewModel.LittleScreenText = "Rel";
        }

        protected override void TimerHandler(object state)
        {
            Log.Debug("Timer tick");
            Counter++;
            ExitechDeviceViewModel.TemperatureScale = ExitechDeviceViewModel.TemperatureScale == TemperatureScale.Celsius
                ? TemperatureScale.Fahrenheit
                : TemperatureScale.Celsius;
        }
    }
}