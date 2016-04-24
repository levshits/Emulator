using System.Windows;
using Emulator.Model;

namespace Emulator.ViewModel
{
    public class HannaDeviceViewModel: ViewModelBase, IDeviceViewModel
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
        public string DeviceName => "Hanna";

        public void OnInit()
        {
            LittleScreenText = 100.ToString();
            BigScreenText = 200.ToString();
            Mode = HannaScaleMode.Ph;
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
            get { return TemperatureScale == TemperatureScale.Celsius ? Visibility.Visible : Visibility.Hidden; ; }
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
    }
}