using System;
using System.Windows;
using Emulator.Logic.Exitec;
using Emulator.ViewModel;

namespace Emulator.Logic
{
    public class ExitecOnOffButtonCommand: CommandWithDelay
    {
        public ExitecDeviceViewModel ExitecDeviceViewModel { get; set; }
        public ExitecStateMachine ExitecStateMachine { get; set; }

        public override void DoReleaseExecute()
        {
            base.DoReleaseExecute();
            ExitecDeviceViewModel.LittleScreenText = "Rel";
        }

        public override void DoClickExecute()
        {
            ExitecStateMachine.Fire(ExitecDeviceTriggers.OnOffButtonClick);
        }

        protected override void TimerHandler(object state)
        {
            Log.Debug("Timer tick");
            Counter++;
            ExitecDeviceViewModel.BigScreenText = Counter.ToString();
        }
    }
}