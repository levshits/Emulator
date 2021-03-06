﻿using System;
using System.Collections.Generic;
using Emulator.Common.Models;

namespace Emulator.Common.Interfaces
{
    /// <summary>
    /// Интерфейс провайдера данных
    /// </summary>
    public interface IDataProvider
    {
        void ApplyFilter(FilterModel filter);
        void Reset(VariantModel variant);
        List<VariantModel> GetVariants();
        List<FilterModel> GetFilters();
        void OnInit();

        event EventHandler DataChanged;
        VariantModel GetData();
    }
}