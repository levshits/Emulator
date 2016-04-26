using Emulator.Common;
using Emulator.ViewModel;
using Stateless;

namespace Emulator.Logic.Hanna
{
    public class HannaStateMachine: DeviceStateMachineBase<HannaDeviceStates, HannaDeviceTriggers>
    {
        public HannaDeviceViewModel HannaDeviceViewModel { get; set; }
        public override void Configure()
        {
            StateMachine = new StateMachine<HannaDeviceStates, HannaDeviceTriggers>(HannaDeviceStates.Disabled);
            StateMachine.Configure(HannaDeviceStates.Disabled)
                .OnEntry(HannaDeviceViewModel.EnableDeice)
                .Permit(HannaDeviceTriggers.OnModeButtonClick, HannaDeviceStates.Enabled)
                .Ignore(HannaDeviceTriggers.OnModeButtonLongClick);

            StateMachine.Configure(HannaDeviceStates.Enabled)
                .OnEntry(HannaDeviceViewModel.DisableDevice)
                .Permit(HannaDeviceTriggers.OnModeButtonClick, HannaDeviceStates.Disabled)
                .Ignore(HannaDeviceTriggers.OnModeButtonLongClick);

        }
    }
}