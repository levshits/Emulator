using System.Collections.Generic;
using Common.Logging;
using Emulator.Common.Interfaces;
using Emulator.Common.Models;

namespace Emulator.Data
{
    public class DataProviderStub: IDataProvider
    {
        private static readonly ILog Log = LogManager.GetLogger<IDataProvider>();
        public void Reset()
        {
            Log.Debug("Reset data implemented");
        }

        public List<VariantModel> GetVariants()
        {
            return new List<VariantModel>() {new VariantModel() {Id = 1, Name = "Variant 1"}, new VariantModel() {Id = 2, Name = "Variant 2"} };
        }
    }
}