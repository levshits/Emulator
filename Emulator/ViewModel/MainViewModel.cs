using System.Collections.Generic;
using System.Windows.Input;
using Emulator.Common.Interfaces;
using Emulator.Common.Models;
using Emulator.Logic;

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

        public ExitechDeviceViewModel ExitechDeviceViewModel { get; set; }
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
        public ICommand ApplyFilterCommand { get; set; }

        public MainViewModel()
        {
            
        }
        public MainViewModel(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void OnInit()
        {
            SetDeviceModel();
            Variants = _dataProvider.GetVariants();
            FilterModels = _dataProvider.GetFilters();
        }

        private void SetDeviceModel()
        {
            DeviceModel = IsExitecSelected ? (IDeviceViewModel) ExitechDeviceViewModel : HannaDeviceViewModel;
        }
    }
}