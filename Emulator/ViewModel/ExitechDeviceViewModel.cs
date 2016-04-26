using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Emulator.Logic;
using Emulator.Logic.Exitech;

namespace Emulator.ViewModel
{
    public class ExitechDeviceViewModel : ViewModelBase, IDeviceViewModel
    {
        private const int MEMORY_SIZE = 25;
        private const string PERCENT = "%";
        private const string PROMILLE = "ppm";
        private const string MG_PER_L = "mg/L";
        private string _bigScreenText;
        private string _littleScreenText;
        private TemperatureScale _temperatureScale;
        private decimal _histogramMinValue;
        private decimal _histogramMaxValue;
        private decimal _historgamValue;
        private Visibility _deviceScreenVisibility;
        private string _histogramScale;
        private Visibility _holdIndicatorVisibility;
        private bool _isTemperatureScaleVisible;
        public string DeviceName => "Exitech";
        public ICommand ExitechModeHoldButtonCommand { get; set; }
        public ICommand ExitechCallRecallButtonCommand { get; set; }
        public ICommand ExitechOnOffButtonCommand { get; set; }
        public ExitechStateMachine ExitechStateMachine { get; set; }

        public void OnInit()
        {
            TemperatureScale = TemperatureScale.Celsius;
            HistogramMinValue = 0;
            HistogramMaxValue = 100;
            HistorgamValue = 50;
            DeviceScreenVisibility = Visibility.Hidden;
            HistogramScale = PERCENT;
            IsTemperatureScaleVisible = false;
            HoldIndicatorVisibility = Visibility.Hidden;
        }

        #region Properties

        public List<HistoryModel> ValuesHistory { get; set; } = new List<HistoryModel>();

        public bool IsTemperatureScaleVisible
        {
            get { return _isTemperatureScaleVisible; }
            set
            {
                _isTemperatureScaleVisible = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TemperatureScale));
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
            get
            {
                return TemperatureScale == TemperatureScale.Fahrenheit && IsTemperatureScaleVisible
                    ? Visibility.Visible
                    : Visibility.Hidden;
            }
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
                return TemperatureScale == TemperatureScale.Celsius && IsTemperatureScaleVisible
                    ? Visibility.Visible
                    : Visibility.Hidden;
                ;
            }
            set
            {
                TemperatureScale = value == Visibility.Visible ? TemperatureScale.Celsius : TemperatureScale.Fahrenheit;
                OnPropertyChanged();
            }
        }

        public Visibility HoldIndicatorVisibility
        {
            get { return _holdIndicatorVisibility; }
            set
            {
                _holdIndicatorVisibility = value;
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

        public ExitechDeviceViewModel()
        {
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

        public decimal TemperatureInCelsius
        {
            get { return _temperatureInCelsius; }
            set
            {
                _temperatureInCelsius = value;
                OnPropertyChanged();
            }
        }

        public decimal OxygenInPercents
        {
            get { return _oxygenInPercents; }
            set
            {
                _oxygenInPercents = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties

        #region Actions

        public void EnableDevice()
        {
            // replace with getting info from data provider
            IsTemperatureScaleVisible = true;
            HistoryItem = null;
            OxygenInPercents = 50;

            HistogramMinValue = measureLimits[GetMeasureSetting()].Key;
            HistogramMaxValue = measureLimits[GetMeasureSetting()].Value;
            HistorgamValue = ConvertMeasureValue(OxygenInPercents, PERCENT, GetMeasureSetting());
            TemperatureInCelsius = (decimal) 28.9;
            BigScreenText = ConvertValueToString(ConvertMeasureValue(OxygenInPercents, PERCENT, GetMeasureSetting()));
            LittleScreenText =
                ConvertTemperatureToString(TemperatureScale == TemperatureScale.Celsius
                    ? TemperatureInCelsius
                    : ConvertCelsiusToFarengeit(TemperatureInCelsius));
            HistogramScale = GetMeasureSetting();
        }

        public void DisableDevice()
        {
            DeviceScreenVisibility = Visibility.Hidden;
        }

        public async Task Initialize()
        {
            DeviceScreenVisibility = Visibility.Visible;
            BigScreenText = "SELF";
            LittleScreenText = "CAL";
            await Task.Delay(3000);
            ExitechStateMachine.Fire(ExitechDeviceTriggers.TimerTick);
        }

        public async Task OnClear()
        {
            BigScreenText = "CLR";
            LittleScreenText = "";
            ValuesHistory.Clear();
            await Task.Delay(3000);
            ExitechStateMachine.Fire(ExitechDeviceTriggers.TimerTick);
        }

        public void OnHoldEntry()
        {
            //Memory contains only 25 symbols
            ValuesHistory.Insert(ValuesHistory.Count%MEMORY_SIZE,
                new HistoryModel() {MeasureSetting = GetMeasureSetting(), Value = Decimal.Parse(BigScreenText)});
            LittleScreenText = (ValuesHistory.Count%MEMORY_SIZE).ToString();
            IsTemperatureScaleVisible = false;
            HoldIndicatorVisibility = Visibility.Visible;
        }

        #endregion Actions

        public void OnHoldExit()
        {
            HoldIndicatorVisibility = Visibility.Hidden;
        }

        public int? HistoryItem { get; set; }

        public async Task OnHistoryEntry()
        {
            if (ValuesHistory.Any())
            {
                HistoryItem = HistoryItem ?? 0;
                LittleScreenText = (HistoryItem + 1).ToString();
                BigScreenText = ConvertValueToString(ConvertToCurrentValue(ValuesHistory[HistoryItem.Value]));
                IsTemperatureScaleVisible = false;
                HistoryItem = (HistoryItem + 1)%ValuesHistory.Count;
            }
            else
            {
                BigScreenText = "END";
                LittleScreenText = "";
                await Task.Delay(2000);
                ExitechStateMachine.Fire(ExitechDeviceTriggers.CallRecallClick);
            }
        }

        public void OnMeasureSettingEntry()
        {
            IsTemperatureScaleVisible = false;
            var measureSetting = GetNextMeasureSetting();
            HistogramScale = measureSetting;
            BigScreenText = measureSetting;
            LittleScreenText = "";
        }

        public int MeasureIndex { get; set; }
        private readonly List<string> _measureSetting = new List<string> {PERCENT, PROMILLE, MG_PER_L};

        private readonly Dictionary<string, KeyValuePair<decimal, decimal>> measureLimits = new Dictionary
            <string, KeyValuePair<decimal, decimal>>
        {
            {PERCENT, new KeyValuePair<decimal, decimal>(0, 200)},
            {PROMILLE, new KeyValuePair<decimal, decimal>(0, 20)},
            {MG_PER_L, new KeyValuePair<decimal, decimal>(0, 20)},
        };

        private decimal _temperatureInCelsius;
        private decimal _oxygenInPercents;

        private string GetNextMeasureSetting()
        {
            MeasureIndex++;
            return GetMeasureSetting();
        }

        private string GetMeasureSetting()
        {
            return _measureSetting[MeasureIndex%_measureSetting.Count];
        }

        public async Task OnHistoryExit()
        {
            await Task.Delay(1000);
        }

        private decimal ConvertToCurrentValue(HistoryModel model)
        {
            var currentMeasure = GetMeasureSetting();
            return ConvertMeasureValue(model.Value, model.MeasureSetting, currentMeasure);
        }

        private static decimal ConvertMeasureValue(decimal value, string sourceMeasure, string currentMeasure)
        {
            decimal convertToCurrentValue = 0;
            if (sourceMeasure == currentMeasure)
            {
                convertToCurrentValue = value;
            }
            else
            {
                if (sourceMeasure == PERCENT && currentMeasure == PROMILLE)
                {
                    convertToCurrentValue = value/10;
                }
                else if (sourceMeasure == PROMILLE && currentMeasure == PERCENT)
                {
                    convertToCurrentValue = value*10;
                }
                else if (sourceMeasure == PERCENT && currentMeasure == MG_PER_L)
                {
                    convertToCurrentValue = value/10;
                }
                else if (sourceMeasure == PROMILLE && currentMeasure == MG_PER_L)
                {
                    convertToCurrentValue = value;
                }
                else if (sourceMeasure == MG_PER_L && currentMeasure == PROMILLE)
                {
                    convertToCurrentValue = value;
                }
                else if (sourceMeasure == MG_PER_L && currentMeasure == PERCENT)
                {
                    convertToCurrentValue = value*10;
                }
            }
            return convertToCurrentValue;
        }

        private static decimal ConvertCelsiusToFarengeit(decimal value)
        {
            return value*(decimal) 1.80 + 32;
        }

        private static string ConvertTemperatureToString(decimal value)
        {
            return $"{value:##.0}";
        }

        private string ConvertValueToString(decimal value)
        {
            var currentMeasure = GetMeasureSetting();
            if (currentMeasure == PROMILLE)
            {
                return $"{value:##.00}";
            }
            else if (currentMeasure == PERCENT)
            {
                return $"{value:###.0}";
            }
            else if (currentMeasure == MG_PER_L)
            {
                return $"{value:##.00}";
            }
            return String.Empty;
        }

        public class HistoryModel
        {
            public decimal Value { get; set; }
            public string MeasureSetting { get; set; }
        }
    }
}