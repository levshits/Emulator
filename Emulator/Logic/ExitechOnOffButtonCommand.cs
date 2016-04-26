using System;
using System.Windows;
using Emulator.Logic.Exitech;
using Emulator.ViewModel;

namespace Emulator.Logic
{
    public class ExitechOnOffButtonCommand: CommandWithDelay
    {
        public ExitechDeviceViewModel ExitechDeviceViewModel { get; set; }
        public ExitechStateMachine ExitechStateMachine { get; set; }

        public override void DoReleaseExecute()
        {
            base.DoReleaseExecute();
            ExitechDeviceViewModel.LittleScreenText = "Rel";
        }

        public override void DoClickExecute()
        {
            ExitechStateMachine.Fire(ExitechDeviceTriggers.OnOffButtonClick);
        }

        protected override void TimerHandler(object state)
        {
            Log.Debug("Timer tick");
            Counter++;
            ExitechDeviceViewModel.BigScreenText = Counter.ToString();
        }
    }
}