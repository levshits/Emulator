namespace Emulator.Common.Models
{
    public class FilterModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double OxygenCoefficient { get; set; }
        public double ECCoefficient { get; set; }
        public double TDSCoefficient { get; set; }
        public double PHCoefficient { get; set; }
        public double TemperatureCoefficient { get; set; }
    }
}
