namespace Emulator.Logic.Hanna
{
    /// <summary>
    /// Список обрабатываемых событий для прибора Hanna
    /// </summary>
    public enum HannaDeviceTriggers
    {
         OnModeButtonClick,
         OnModeButtonLongClick,
         OnModeButtonDoubleClick,
         OnModeButtonRelease,
         SetHoldButtonClick,
         SetHoldButtonLongClick,
         SetHoldButtonDoubleClick,
         SetHoldButtonRelease,
         TimerTick,
         DataChanged
    }
}