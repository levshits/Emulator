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
                .Permit(HannaDeviceTriggers.OnModeButtonDoubleClick, HannaDeviceStates.Loading);
            
            StateMachine.Configure(HannaDeviceStates.Loading)
                .OnEntry(async ()=> await HannaDeviceViewModel.Initialize())
                .Permit(HannaDeviceTriggers.TimerTick, HannaDeviceStates.Measure);

            StateMachine.Configure(HannaDeviceStates.Measure)
                .SubstateOf(HannaDeviceStates.Enabled)
                .OnEntry(HannaDeviceViewModel.EnableDevice)
                .OnEntryFrom(HannaDeviceTriggers.SetHoldButtonClick, HannaDeviceViewModel.OnSetHoldButtonClickInMeasureState)
                .PermitReentry(HannaDeviceTriggers.DataChanged)
                .PermitReentry(HannaDeviceTriggers.SetHoldButtonClick)
                .Permit(HannaDeviceTriggers.OnModeButtonLongClick, HannaDeviceStates.TempSettings)
                .Permit(HannaDeviceTriggers.SetHoldButtonLongClick, HannaDeviceStates.Hold);

            StateMachine.Configure(HannaDeviceStates.Hold)
                .SubstateOf(HannaDeviceStates.Enabled)
                .OnEntry(HannaDeviceViewModel.OnHoldEntry)
                .Permit(HannaDeviceTriggers.OnModeButtonClick, HannaDeviceStates.Measure)
                .Permit(HannaDeviceTriggers.SetHoldButtonClick, HannaDeviceStates.Measure);

            StateMachine.Configure(HannaDeviceStates.TempSettings)
                .SubstateOf(HannaDeviceStates.Enabled)
                .OnEntry(HannaDeviceViewModel.OnTempSettingEntry)
                .OnEntryFrom(HannaDeviceTriggers.SetHoldButtonClick,
                    HannaDeviceViewModel.OnSetHoldButtonClickInTempSettingsState)
                .PermitReentry(HannaDeviceTriggers.SetHoldButtonClick)
                .Permit(HannaDeviceTriggers.OnModeButtonLongClick, HannaDeviceStates.Disabling)
                .Permit(HannaDeviceTriggers.OnModeButtonDoubleClick, HannaDeviceStates.Measure);

            StateMachine.Configure(HannaDeviceStates.Disabling)
                .OnEntry(HannaDeviceViewModel.OnDisablingEntry)
                .Permit(HannaDeviceTriggers.OnModeButtonLongClick, HannaDeviceStates.Measure)
                .Permit(HannaDeviceTriggers.OnModeButtonRelease, HannaDeviceStates.Disabled);


        }
    }
}