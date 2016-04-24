using System.Collections.Generic;
using Emulator.Common.Models;

namespace Emulator.Common.Interfaces
{
    public interface IDataProvider
    {
        void Reset(VariantModel variant);
        List<VariantModel> GetVariants();
        List<FilterModel> GetFilters();
        void OnInit();
    }
}