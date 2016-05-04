using Common.Logging;
using Emulator.Common;
using Emulator.ViewModel;
using Stateless;

namespace Emulator.Logic.Exitech
{
    /// <summary>
    /// Машина состояний для прибора Exitech
    /// </summary>
    public class ExitechStateMachine : DeviceStateMachineBase<ExitechDeviceStates, ExitechDeviceTriggers>
    {
        protected static readonly ILog Log = LogManager.GetLogger<ExitechStateMachine>();
        protected ExitechDeviceViewModel ExitechDeviceViewModel { get; set; }

        public override void Configure()
        {
            StateMachine = new StateMachine<ExitechDeviceStates, ExitechDeviceTriggers>(ExitechDeviceStates.Disabled);

            StateMachine.OnUnhandledTrigger(
                ((states, triggers) =>
                {
                    Log.Warn(string.Format("The trigger has been ignored: state ={0}, trigger={1}", states, triggers));
                }));

            StateMachine.OnTransitioned((state) => { Log.Debug("Transition finished"); });

            StateMachine.Configure(ExitechDeviceStates.Disabled)
                .OnEntry(ExitechDeviceViewModel.DisableDevice)
                .Permit(ExitechDeviceTriggers.OnOffButtonClick, ExitechDeviceStates.Loading);

            StateMachine.Configure(ExitechDeviceStates.Loading)
                .OnEntry(async () => await ExitechDeviceViewModel.Initialize())
                .Permit(ExitechDeviceTriggers.TimerTick, ExitechDeviceStates.Measure);

            StateMachine.Configure(ExitechDeviceStates.Measure)
                .SubstateOf(ExitechDeviceStates.Enabled)
                .OnEntry(ExitechDeviceViewModel.EnableDevice)
                .PermitReentry(ExitechDeviceTriggers.DataChanged)
                .Permit(ExitechDeviceTriggers.OnOffButtonClick, ExitechDeviceStates.Disabled)
                .Permit(ExitechDeviceTriggers.ModeHoldClick, ExitechDeviceStates.Hold)
                .Permit(ExitechDeviceTriggers.Clear, ExitechDeviceStates.Clear)
                .Permit(ExitechDeviceTriggers.ModeHoldTimerTick, ExitechDeviceStates.MeasureSetting)
                .Permit(ExitechDeviceTriggers.CallRecallClick, ExitechDeviceStates.History)
                .Permit(ExitechDeviceTriggers.CallRecallTimerTick, ExitechDeviceStates.TemperatureSetting);

            StateMachine.Configure(ExitechDeviceStates.Clear)
                .SubstateOf(ExitechDeviceStates.Enabled)
                .OnEntry(async () => await ExitechDeviceViewModel.OnClear())
                .Permit(ExitechDeviceTriggers.TimerTick, ExitechDeviceStates.Measure);

            StateMachine.Configure(ExitechDeviceStates.Hold)
                .SubstateOf(ExitechDeviceStates.Enabled)
                .OnEntry(ExitechDeviceViewModel.OnHoldEntry)
                .OnExit(ExitechDeviceViewModel.OnHoldExit)
                .Permit(ExitechDeviceTriggers.OnOffButtonClick, ExitechDeviceStates.Disabled)
                .Permit(ExitechDeviceTriggers.ModeHoldClick, ExitechDeviceStates.Measure)
                .Permit(ExitechDeviceTriggers.Clear, ExitechDeviceStates.Clear);

            StateMachine.Configure(ExitechDeviceStates.History)
                .SubstateOf(ExitechDeviceStates.Enabled)
                .OnEntry(async () => await ExitechDeviceViewModel.OnHistoryEntry())
                .PermitReentry(ExitechDeviceTriggers.ModeHoldClick)
                .Permit(ExitechDeviceTriggers.CallRecallClick, ExitechDeviceStates.Measure)
                .Permit(ExitechDeviceTriggers.OnOffButtonClick, ExitechDeviceStates.Disabled)
                .Permit(ExitechDeviceTriggers.Clear, ExitechDeviceStates.Clear);

            StateMachine.Configure(ExitechDeviceStates.MeasureSetting)
                .SubstateOf(ExitechDeviceStates.Enabled)
                .OnEntry(ExitechDeviceViewModel.OnMeasureSettingEntry)
                .OnExit(async () => await ExitechDeviceViewModel.OnHistoryExit())
                .PermitReentry(ExitechDeviceTriggers.ModeHoldTimerTick)
                .Permit(ExitechDeviceTriggers.ModeHoldRelease, ExitechDeviceStates.Measure)
                .Permit(ExitechDeviceTriggers.OnOffButtonClick, ExitechDeviceStates.Disabled)
                .Permit(ExitechDeviceTriggers.Clear, ExitechDeviceStates.Clear);

            StateMachine.Configure(ExitechDeviceStates.TemperatureSetting)
                .SubstateOf(ExitechDeviceStates.Enabled)
                .OnEntry(ExitechDeviceViewModel.OnTemperatureSettingEntry)
                .OnExit(async () => await ExitechDeviceViewModel.OnHistoryExit())
                .PermitReentry(ExitechDeviceTriggers.CallRecallTimerTick)
                .Permit(ExitechDeviceTriggers.CallRecallRelease, ExitechDeviceStates.Measure)
                .Permit(ExitechDeviceTriggers.OnOffButtonClick, ExitechDeviceStates.Disabled)
                .Permit(ExitechDeviceTriggers.Clear, ExitechDeviceStates.Clear);
        }
    }
}