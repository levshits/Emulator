using System.Windows;

namespace Emulator.ViewModel
{
    public class HannaDeviceViewModel: ViewModelBase, IDeviceViewModel
    {
        private TemperatureScale _temperatureScale;
        private string _bigScreenText;
        private string _littleScreenText;
        public string DeviceName => "Hanna";

        public void OnInit()
        {
            LittleScreenText = 100.ToString();
            BigScreenText = 200.ToString();
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
    }
}