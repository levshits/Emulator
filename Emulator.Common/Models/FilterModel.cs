namespace Emulator.Common.Models
{
    public class FilterModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double OxygenCoefficient { get; set; }
        public double ConductivityCoefficient { get; set; }
        public double PhCoefficient { get; set; }
    }
}
