using System.Collections.Generic;
using System.Windows.Input;
using Emulator.Common.Interfaces;
using Emulator.Common.Models;
using Emulator.Logic;
using Emulator.Models;

namespace Emulator.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataProvider _dataProvider;
        private bool _isExitecSelected = true;
        private IDeviceViewModel _deviceModel;
        private VariantModel _variant;

        public bool IsExitecSelected
        {
            get { return _isExitecSelected; }
            set
            {
                _isExitecSelected = value;
                SetDeviceModel();
                OnPropertyChanged();
            }
        }

        public IDeviceViewModel DeviceModel
        {
            get { return _deviceModel; }
            set
            {
                _deviceModel = value;
                OnPropertyChanged();
            }
        }

        public ExitecDeviceViewModel ExitecDeviceViewModel { get; set; }
        public HannaDeviceViewModel HannaDeviceViewModel { get; set; }
        public List<FilterModel> FilterModels { get; set; }
        public List<VariantModel> Variants { get; set; }

        public VariantModel Variant
        {
            get { return _variant; }
            set
            {
                _variant = value;
                OnPropertyChanged();
            }
        }

        public ICommand ResetCommand { get; set; }

        public MainViewModel()
        {
            
        }
        public MainViewModel(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
            FilterModels = new List<FilterModel>()
            {
                new FilterModel() {Description = "Description 1", FilterName = "Filter1"},
                new FilterModel() {Description = "Description 2", FilterName = "Filter2"},
            };
        }

        public void OnInit()
        {
            SetDeviceModel();
            Variants = _dataProvider.GetVariants();
        }

        private void SetDeviceModel()
        {
            DeviceModel = IsExitecSelected ? (IDeviceViewModel) ExitecDeviceViewModel : HannaDeviceViewModel;
        }
    }
}