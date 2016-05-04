using Stateless;

namespace Emulator.Common.Interfaces
{
    /// <summary>
    /// Интерфейс машины состояний прибора
    /// </summary>
    /// <typeparam name="TState">Перечисление возможных состояний</typeparam>
    /// <typeparam name="TTrigger">Перечислление возможных событий</typeparam>
    public interface IDeviceStateMachine<TState, TTrigger>
    {
        StateMachine<TState, TTrigger> StateMachine { get; set; }
        void Fire(TTrigger action);
        void Fire<T>(TTrigger action, T value);
        void Configure();
    }
}