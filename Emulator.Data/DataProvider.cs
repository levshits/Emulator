using System.Collections.Generic;
using Emulator.Common.Interfaces;
using Emulator.Common.Models;

namespace Emulator.Data
{
    public class DataProvider: IDataProvider
    {
        private const string DATA_FILE = @"~/Config/Data.xls"; 
        public void Reset(VariantModel variant)
        {
            throw new System.NotImplementedException();
        }

        public List<VariantModel> GetVariants()
        {
            throw new System.NotImplementedException();
        }

        public List<FilterModel> GetFilters()
        {
            throw new System.NotImplementedException();
        }

        public void OnInit()
        {
            
        }
    }
}