﻿using System.Globalization;
using System.Windows;
using System.Windows.Input;
using Emulator.Logic;

namespace Emulator.ViewModel
{
    public class ExitecDeviceViewModel : ViewModelBase, IDeviceViewModel
    {
        private string _bigScreenText;
        private string _littleScreenText;
        private TemperatureScale _temperatureScale;
        private decimal _histogramMinValue;
        private decimal _histogramMaxValue;
        private decimal _historgamValue;
        private Visibility _deviceScreenVisibility;
        private string _histogramScale;
        public string DeviceName => "Exitec";
        public ICommand ExitecModeHoldButtonCommand { get; set; }
        public ICommand ExitecCallRecallButtonCommand { get; set; }
        public ICommand ExitecOnOffButtonCommand { get; set; }

        public void OnInit()
        {
            TemperatureScale = TemperatureScale.Celsius;
            HistogramMinValue = 0;
            HistogramMaxValue = 100;
            HistorgamValue = 50;
            DeviceScreenVisibility = Visibility.Hidden;
            HistogramScale = "%";
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

        public Visibility FahrengeitIndicatorVisibility {
            get { return TemperatureScale == TemperatureScale.Fahrenheit? Visibility.Visible : Visibility.Hidden; }
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

        public ExitecDeviceViewModel()
        {
            BigScreenText = "Init";
            LittleScreenText = "Cal";
        }

        public decimal HistogramMinValue
        {
            get { return _histogramMinValue; }
            set
            {
                _histogramMinValue = value; 
                OnPropertyChanged();
                OnPropertyChanged(nameof(HistogramMinLabel));
            }
        }
        public string HistogramMinLabel => _histogramMinValue.ToString(CultureInfo.InvariantCulture);

        public decimal HistogramMaxValue
        {
            get { return _histogramMaxValue; }
            set
            {
                _histogramMaxValue = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HistogramMaxLabel));
            }
        }
        public string HistogramMaxLabel => _histogramMaxValue.ToString(CultureInfo.InvariantCulture);

        public decimal HistorgamValue
        {
            get { return _historgamValue; }
            set
            {
                _historgamValue = value; 
                OnPropertyChanged();
                OnPropertyChanged(nameof(HistorgamValueToBind));
            }
        }

        public decimal HistorgamValueToBind
        {
            get { return _historgamValue/(_histogramMaxValue - _histogramMinValue)*180; }
            set { _historgamValue = _histogramMinValue + value/180*(_histogramMaxValue - _histogramMinValue); }
        }

        public string HistogramScale
        {
            get { return _histogramScale; }
            set
            {
                _histogramScale = value; 
                OnPropertyChanged();
            }
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
    }
}