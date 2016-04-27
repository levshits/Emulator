namespace Emulator.Common.Models
{
    public class VariantModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Oxygen { get; set; }
        public double Conductivity { get; set; }
        public double Ph { get; set; }
        public double Temperature { get; set; }

        public VariantModel CreateCopy()
        {
            return new VariantModel()
            {
                Id = Id,
                Name = Name,
                Oxygen = Oxygen,
                Conductivity = Conductivity,
                Ph = Ph,
                Temperature = Temperature
            };
        }
    }
}