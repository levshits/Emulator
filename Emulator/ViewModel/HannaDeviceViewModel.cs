using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
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
        private Visibility _ppmIndicatorVisibility;
        private Visibility _mscmIndicatorVisibility;
        private Visibility _phIndicatorVisibility;
        private HannaScaleMode _mode;
        private Visibility _deviceScreenVisibility;
        public string DeviceName => "Hanna";
        public ICommand HannaOnModeButtonCommand { get; set; }
        public ICommand HannaSetHoldButtonCommand { get; set; }
        public HannaStateMachine HannaStateMachine { get; set; }

        public void OnInit()
        {
            DeviceScreenVisibility = Visibility.Hidden;
            LittleScreenText = 100.ToString();
            BigScreenText = 200.ToString();
            Mode = HannaScaleMode.Ph;
        }

        #region Properties

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

        public Visibility PpmIndicatorVisibility
        {
            get { return _ppmIndicatorVisibility; }
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
                    _ppmIndicatorVisibility = Visibility.Hidden;
                    _mscmIndicatorVisibility = Visibility.Hidden;
                }
                    break;
                case HannaScaleMode.Ppt:
                {
                    _phIndicatorVisibility = Visibility.Hidden;
                    _pptIndicatorVisibility = Visibility.Visible;
                    _ppmIndicatorVisibility = Visibility.Hidden;
                    _mscmIndicatorVisibility = Visibility.Hidden;
                }
                    break;
                case HannaScaleMode.Ppm:
                {
                    _phIndicatorVisibility = Visibility.Hidden;
                    _pptIndicatorVisibility = Visibility.Hidden;
                    _ppmIndicatorVisibility = Visibility.Visible;
                    _mscmIndicatorVisibility = Visibility.Hidden;
                }
                    break;
                case HannaScaleMode.Mscm:
                {
                    _phIndicatorVisibility = Visibility.Hidden;
                    _pptIndicatorVisibility = Visibility.Hidden;
                    _ppmIndicatorVisibility = Visibility.Hidden;
                    _mscmIndicatorVisibility = Visibility.Visible;
                }
                    break;
            }
            OnPropertyChanged(nameof(PhIndicatorVisibility));
            OnPropertyChanged(nameof(PptIndicatorVisibility));
            OnPropertyChanged(nameof(PpmIndicatorVisibility));
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

        #endregion Properties

        #region Actions

        public void EnableDevice()
        {
            DeviceScreenVisibility = Visibility.Visible;
        }

        public void DisableDevice()
        {
            DeviceScreenVisibility = Visibility.Hidden;
        }
        public void Initialize()
        {
            BigScreenText = "Init";
            Thread.Sleep(500);
            HannaStateMachine.Fire(HannaDeviceTriggers.TimerTick);
        }

        #endregion Actions
    }
}