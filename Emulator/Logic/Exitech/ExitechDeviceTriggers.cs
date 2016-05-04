namespace Emulator.Logic.Exitech
{
    /// <summary>
    /// Список обрабатываемых событий для прибора
    /// </summary>
    public enum ExitechDeviceTriggers
    {
        OnOffButtonClick,
        OnOffButtonDoubleClick,
        OnOffTimerTick,
        Clear,
        ModeHoldClick,
        ModeHoldDoubleClick,
        ModeHoldTimerTick,
        ModeHoldRelease,
        CallRecallClick,
        CallRecallRelease,
        CallRecallDoubleClick,
        CallRecallTimerTick,
        TimerTick,
        DataChanged
    }
}