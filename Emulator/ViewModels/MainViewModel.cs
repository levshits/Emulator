using System;
using System.Collections.Generic;
using Emulator.Models;

namespace Emulator.ViewModels
{
    public class MainViewModel: ViewModelBase
    {
        public ExitecModel DeviceModel { get; set; }
        public List<FilterModel> FilterModels { get; set; }

        public MainViewModel()
        {
            DeviceModel = new ExitecModel();
            FilterModels = new List<FilterModel>()
            {
                new FilterModel() {Description = "Description 1", FilterName = "Filter1"},
                new FilterModel() {Description = "Description 2", FilterName = "Filter2"},
            };
        }
    }
}