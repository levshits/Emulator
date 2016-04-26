using Common.Logging;
using Emulator.Common;
using Emulator.ViewModel;
using Stateless;

namespace Emulator.Logic.Exitech
{
    public class ExitechStateMachine: DeviceStateMachineBase<ExitechDeviceStates, ExitechDeviceTriggers>
    {
        protected static readonly ILog Log = LogManager.GetLogger<ExitechStateMachine>();
        protected ExitechDeviceViewModel ExitechDeviceViewModel { get; set; }
        public override void Configure()
        {
            StateMachine = new StateMachine<ExitechDeviceStates, ExitechDeviceTriggers>(ExitechDeviceStates.Disabled);

            StateMachine.OnUnhandledTrigger(((states, triggers) =>
            {
                Log.Warn(string.Format("The trigger has been ignored: state ={0}, trigger={1}", states, triggers));
            }));

            StateMachine.OnTransitioned((state) =>
            {
                Log.Debug("Transition finished");
            });

            StateMachine.Configure(ExitechDeviceStates.Disabled)
                .OnEntry(ExitechDeviceViewModel.DisableDevice)
                .Permit(ExitechDeviceTriggers.OnOffButtonClick, ExitechDeviceStates.Loading);

            StateMachine.Configure(ExitechDeviceStates.Loading)
                .OnEntry(ExitechDeviceViewModel.Initialize)
                .Permit(ExitechDeviceTriggers.TimerTick, ExitechDeviceStates.Enabled);

            StateMachine.Configure(ExitechDeviceStates.Enabled)
                .OnEntry(ExitechDeviceViewModel.EnableDevice)
                .Permit(ExitechDeviceTriggers.OnOffButtonClick, ExitechDeviceStates.Disabled);

        }
    }
}