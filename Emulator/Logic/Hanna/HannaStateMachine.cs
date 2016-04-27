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
        protected HannaDeviceViewModel HannaDeviceViewModel { get; set; }
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
                .Permit(HannaDeviceTriggers.OnModeButtonClick, HannaDeviceStates.Loading);
            
            StateMachine.Configure(HannaDeviceStates.Loading)
                .OnEntry(async ()=> await HannaDeviceViewModel.Initialize())
                .Permit(HannaDeviceTriggers.TimerTick, HannaDeviceStates.Enabled);

            StateMachine.Configure(HannaDeviceStates.Enabled)
                .OnEntry(HannaDeviceViewModel.EnableDevice)
                .PermitReentry(HannaDeviceTriggers.DataChanged)
                .Permit(HannaDeviceTriggers.OnModeButtonClick, HannaDeviceStates.Disabled);

        }
    }
}