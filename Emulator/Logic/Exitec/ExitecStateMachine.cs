using Emulator.Common;
using Emulator.ViewModel;
using Stateless;

namespace Emulator.Logic.Exitec
{
    public class ExitecStateMachine: DeviceStateMachineBase<ExitecDeviceStates, ExitecDeviceTriggers>
    {
        public ExitecDeviceViewModel ExitecDeviceViewModel { get; set; }
        public override void Configure()
        {
            StateMachine = new StateMachine<ExitecDeviceStates, ExitecDeviceTriggers>(ExitecDeviceStates.Disabled);

            StateMachine.Configure(ExitecDeviceStates.Disabled)
                .OnEntry(ExitecDeviceViewModel.EnableDevice)
                .Permit(ExitecDeviceTriggers.OnOffButtonClick, ExitecDeviceStates.Enabled);

            StateMachine.Configure(ExitecDeviceStates.Enabled)
                .OnEntry(ExitecDeviceViewModel.DisableDevice)
                .Permit(ExitecDeviceTriggers.OnOffButtonClick, ExitecDeviceStates.Disabled);
        }
    }
}