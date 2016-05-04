namespace Emulator.Logic.Exitech
{
    /// <summary>
    /// Список состояний прибора
    /// </summary>
    public enum ExitechDeviceStates
    {
        Enabled,
        Loading,
        Disabled,
        Calibration,
        Hold,
        History,
        Clear,
        Measure,
        MeasureSetting,
        TemperatureSetting
    }
}