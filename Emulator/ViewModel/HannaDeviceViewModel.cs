using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Emulator.Common.Interfaces;
using Emulator.Logic.Hanna;
using Emulator.Model;

namespace Emulator.ViewModel
{
    public class HannaDeviceViewModel : ViewModelBase, IDeviceViewModel
    {
        private TemperatureScale _temperatureScale;
        private string _bigScreenText;
        private string _littleScreenText;
        private Visibility _calIndicatorVisibility;
        private Visibility _pptIndicatorVisibility;
        private Visibility _mscmIndicatorVisibility;
        private Visibility _phIndicatorVisibility;
        private HannaScaleMode _mode;
        private Visibility _deviceScreenVisibility;
        private decimal _temperatureInCelsius;
        private decimal _ec;
        private decimal _tds;
        private decimal _ph;
        private bool _isTemperatureVisible;
        public string DeviceName => "Hanna";
        public ICommand HannaOnModeButtonCommand { get; set; }
        public ICommand HannaSetHoldButtonCommand { get; set; }
        public HannaStateMachine HannaStateMachine { get; set; }
        public IDataProvider DataProvider { get; set; }

        public void OnInit()
        {
            DataProvider.DataChanged += OnChange;
            DeviceScreenVisibility = Visibility.Hidden;
            CalIndicatorVisibility = Visibility.Hidden;
            Mode = HannaScaleMode.Ph;
            TemperatureScale = TemperatureScale.Celsius;
        }

        #region Properties

        public bool IsTemperatureVisible
        {
            get { return _isTemperatureVisible; }
            set
            {
                _isTemperatureVisible = value; 
                OnPropertyChanged();
                OnPropertyChanged(nameof(CelsiusIndicatorVisibility));
                OnPropertyChanged(nameof(FahrengeitIndicatorVisibility));
            }
        }

        public TemperatureScale TemperatureScale
        {
            get { return _temperatureScale; }
            set
            {
                _temperatureScale = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CelsiusIndicatorVisibility));
                OnPropertyChanged(nameof(FahrengeitIndicatorVisibility));
            }
        }

        public Visibility FahrengeitIndicatorVisibility
        {
            get { return TemperatureScale == TemperatureScale.Fahrenheit ? Visibility.Visible : Visibility.Hidden; }
            set
            {
                TemperatureScale = value == Visibility.Visible ? TemperatureScale.Fahrenheit : TemperatureScale.Celsius;
                OnPropertyChanged();
            }
        }

        public Visibility CelsiusIndicatorVisibility
        {
            get
            {
                return TemperatureScale == TemperatureScale.Celsius ? Visibility.Visible : Visibility.Hidden;
                ;
            }
            set
            {
                TemperatureScale = value == Visibility.Visible ? TemperatureScale.Celsius : TemperatureScale.Fahrenheit;
                OnPropertyChanged();
            }
        }

        public string BigScreenText
        {
            get { return _bigScreenText; }
            set
            {
                _bigScreenText = value;
                OnPropertyChanged();
            }
        }

        public string LittleScreenText
        {
            get { return _littleScreenText; }
            set
            {
                _littleScreenText = value;
                OnPropertyChanged();
            }
        }

        public Visibility CalIndicatorVisibility
        {
            get { return _calIndicatorVisibility; }
            set
            {
                _calIndicatorVisibility = value;
                OnPropertyChanged();
            }
        }

        public Visibility PptIndicatorVisibility
        {
            get { return _pptIndicatorVisibility; }
        }

        public Visibility MscmIndicatorVisibility
        {
            get { return _mscmIndicatorVisibility; }
        }

        public Visibility PhIndicatorVisibility
        {
            get { return _phIndicatorVisibility; }
        }

        public HannaScaleMode Mode
        {
            get { return _mode; }
            set
            {
                _mode = value;
                ApplyModeChanges();
            }
        }

        private void ApplyModeChanges()
        {
            switch (Mode)
            {
                case HannaScaleMode.Ph:
                {
                    _phIndicatorVisibility = Visibility.Visible;
                    _pptIndicatorVisibility = Visibility.Hidden;
                    _mscmIndicatorVisibility = Visibility.Hidden;
                }
                    break;
                case HannaScaleMode.Ppt:
                {
                    _phIndicatorVisibility = Visibility.Hidden;
                    _pptIndicatorVisibility = Visibility.Visible;
                    _mscmIndicatorVisibility = Visibility.Hidden;
                }
                    break;
                case HannaScaleMode.Mscm:
                {
                    _phIndicatorVisibility = Visibility.Hidden;
                    _pptIndicatorVisibility = Visibility.Hidden;
                    _mscmIndicatorVisibility = Visibility.Visible;
                }
                    break;
            }
            OnPropertyChanged(nameof(PhIndicatorVisibility));
            OnPropertyChanged(nameof(PptIndicatorVisibility));
            OnPropertyChanged(nameof(MscmIndicatorVisibility));
        }

        public Visibility DeviceScreenVisibility
        {
            get { return _deviceScreenVisibility; }
            set
            {
                _deviceScreenVisibility = value;
                OnPropertyChanged();
            }
        }

        public decimal TemperatureInCelsius
        {
            get { return _temperatureInCelsius; }
            set
            {
                _temperatureInCelsius = value;
                OnPropertyChanged();
            }
        }

        public decimal PH
        {
            get { return _ph; }
            set
            {
                _ph = value;
                OnPropertyChanged();
            }
        }

        public decimal EC
        {
            get { return _ec; }
            set
            {
                _ec = value;
                OnPropertyChanged();
            }
        }

        public decimal TDS { 
            get { return _tds; }
            set
            {
                _tds = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties

        #region Actions

        public void EnableDevice()
        {
            BigScreenText = ConvertValueToString(GetDeviceValue());
            LittleScreenText =
                ConvertTemperatureToString(TemperatureScale == TemperatureScale.Celsius
                    ? TemperatureInCelsius
                    : ConvertCelsiusToFarengeit(TemperatureInCelsius));
            IsTemperatureVisible = true;
        }

        public void DisableDevice()
        {
            DeviceScreenVisibility = Visibility.Hidden;
        }
        public async Task Initialize()
        {
            DeviceScreenVisibility = Visibility.Visible;
            BigScreenText = "Init";
            InitData();
            await Task.Delay(3000);
            HannaStateMachine.Fire(HannaDeviceTriggers.TimerTick);
        }
        private static decimal ConvertCelsiusToFarengeit(decimal value)
        {
            return value * (decimal)1.80 + 32;
        }

        private static string ConvertTemperatureToString(decimal value)
        {
            return $"{value:###.0}";
        }
        private string ConvertValueToString(decimal value)
        {
            if (Mode == HannaScaleMode.Ph)
            {
                return $"{value:##.00}";
            }
            else if (Mode == HannaScaleMode.Ppt)
            {
                return $"{value:###.00}";
            }
            else if (Mode == HannaScaleMode.Mscm)
            {
                return $"{value:##.00}";
            }
            return String.Empty;
        }

        private decimal GetDeviceValue()
        {
            if (Mode == HannaScaleMode.Ph)
            {
                return PH;
            }
            else if (Mode == HannaScaleMode.Ppt)
            {
                return TDS;
            }
            else if (Mode == HannaScaleMode.Mscm)
            {
                return EC;
            }
            return 0;
        }

        private void OnChange(object sender, EventArgs eventArgs)
        {
            InitData();
        }
        private void InitData()
        {
            var data = DataProvider.GetData();
            TemperatureInCelsius = (decimal)data.Temperature;
            TDS = (decimal)data.TDS;
            EC = (decimal)data.EC;
            PH = (decimal)data.PH;
            HannaStateMachine.Fire(HannaDeviceTriggers.DataChanged);
        }
        #endregion Actions
    }
}