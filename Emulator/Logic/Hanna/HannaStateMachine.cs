using Common.Logging;
using Emulator.Common;
using Emulator.Logic.Exitech;
using Emulator.ViewModel;
using Stateless;

namespace Emulator.Logic.Hanna
{
    public class HannaStateMachine: DeviceStateMachineBase<HannaDeviceStates, HannaDeviceTriggers>
    {
        protected static readonly ILog Log = LogManager.GetLogger<HannaStateMachine>();
        public HannaDeviceViewModel HannaDeviceViewModel { get; set; }
        public override void Configure()
        {
            StateMachine = new StateMachine<HannaDeviceStates, HannaDeviceTriggers>(HannaDeviceStates.Disabled);

            StateMachine.OnUnhandledTrigger(((states, triggers) =>
            {
                Log.Warn(string.Format("The trigger has been ignored: state ={0}, trigger={1}", states, triggers));
            }));

            StateMachine.OnTransitioned((state) =>
            {
                Log.Debug("Transition finished");
            });

            StateMachine.Configure(HannaDeviceStates.Disabled)
                .OnEntry(HannaDeviceViewModel.DisableDevice)
                .Permit(HannaDeviceTriggers.OnModeButtonClick, HannaDeviceStates.Enabled)
                .Ignore(HannaDeviceTriggers.OnModeButtonLongClick);

            StateMachine.Configure(HannaDeviceStates.Enabled)
                .OnEntry(HannaDeviceViewModel.EnableDevice)
                .Permit(HannaDeviceTriggers.OnModeButtonClick, HannaDeviceStates.Disabled)
                .Ignore(HannaDeviceTriggers.OnModeButtonLongClick);

        }
    }
}