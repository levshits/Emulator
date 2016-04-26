using Emulator.Common.Interfaces;
using Stateless;

namespace Emulator.Common
{
    public abstract class DeviceStateMachineBase<TState, TTrigger>: IDeviceStateMachine<TState, TTrigger>
    {
        public StateMachine<TState, TTrigger> StateMachine { get; set; }
        public void Fire(TTrigger action)
        {
            StateMachine.Fire(action);
        }

        public void Fire<T>(TTrigger action, T value)
        {
            var assignTrigger = StateMachine.SetTriggerParameters<T>(action);
            StateMachine.Fire(assignTrigger, value);
        }

        public abstract void Configure();
    }
}