using System.Collections.Generic;
using System.Windows.Input;
using Emulator.Logic;
using Emulator.Models;

namespace Emulator.ViewModel
{
    public class MainViewModel: ViewModelBase
    {
        public ExitecModel DeviceModel { get; set; }
        public List<FilterModel> FilterModels { get; set; }
        public ICommand ResetCommand { get; set; }

        public MainViewModel()
        {
            DeviceModel = new ExitecModel();
            FilterModels = new List<FilterModel>()
            {
                new FilterModel() {Description = "Description 1", FilterName = "Filter1"},
                new FilterModel() {Description = "Description 2", FilterName = "Filter2"},
            };
            ResetCommand = new ResetCommand();
        }
    }
}