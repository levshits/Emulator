namespace Emulator.Common.Models
{
    public class VariantModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Oxygen { get; set; }
        public double EC { get; set; }
        public double PH { get; set; }
        public double TDS { get; set; }
        public double Temperature { get; set; }

        public VariantModel CreateCopy()
        {
            return new VariantModel()
            {
                Id = Id,
                Name = Name,
                Oxygen = Oxygen,
                EC = EC,
                PH = PH,
                TDS = TDS,
                Temperature = Temperature
            };
        }
    }
}