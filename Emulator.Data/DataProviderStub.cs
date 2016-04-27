using System;
using System.Collections.Generic;
using Common.Logging;
using Emulator.Common.Interfaces;
using Emulator.Common.Models;

namespace Emulator.Data
{
    public class DataProviderStub: IDataProvider
    {
        private static readonly ILog Log = LogManager.GetLogger<IDataProvider>();
        public void ApplyFilter(FilterModel filter)
        {
        }

        public void Reset(VariantModel model)
        {
            Log.Debug("Reset data implemented");
        }

        public List<VariantModel> GetVariants()
        {
            return new List<VariantModel>() {new VariantModel() {Id = 1, Name = "Variant 1"}, new VariantModel() {Id = 2, Name = "Variant 2"} };
        }

        public List<FilterModel> GetFilters()
        {
            return new List<FilterModel>()
            {
                new FilterModel() {Description = "Description 1", Name = "Filter1"},
                new FilterModel() {Description = "Description 2 Description 2 Description 2 Description 2", Name = "Filter2"},
            };
        }

        public void OnInit()
        {
        }

        public event EventHandler DataChanged;
        public VariantModel GetData()
        {
            throw new NotImplementedException();
        }
    }
}