using System.Collections.Generic;
using Emulator.Common.Models;

namespace Emulator.Common.Interfaces
{
    public interface IDataProvider
    {
        void Reset();
        List<VariantModel> GetVariants();
    }
}