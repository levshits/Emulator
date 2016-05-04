namespace Emulator.Logic.Hanna
{
    /// <summary>
    /// Список состояний прибора
    /// </summary>
    public enum HannaDeviceStates
    {
        Enabled,
        Loading,
        Disabled,
        Measure,
        TempSettings,
        Hold,
        Disabling
    }
}