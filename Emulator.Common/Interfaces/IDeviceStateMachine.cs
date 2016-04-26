using Stateless;

namespace Emulator.Common.Interfaces
{
    public interface IDeviceStateMachine<TState, TTrigger>
    {
        StateMachine<TState, TTrigger> StateMachine { get; set; }
        void Fire(TTrigger action);
        void Fire<T>(TTrigger action, T value);
        void Configure();
    }
}