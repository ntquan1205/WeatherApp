using WeatherApp.Models;

namespace WeatherApp.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private Settings _settings = new Settings();

        private TemperatureUnit _temperatureUnit;
        private PressureUnit _pressureUnit;
        private WindSpeedUnit _windSpeedUnit;
        private PrecipitationUnit _precipitationUnit;

        public TemperatureUnit TemperatureUnit
        {
            get => _temperatureUnit;
            set
            {
                if (SetField(ref _temperatureUnit, value))
                {
                    _settings.TemperatureUnit = value;
                    OnPropertyChanged(nameof(IsCelsius));
                    OnPropertyChanged(nameof(IsFahrenheit));
                }
            }
        }

        public PressureUnit PressureUnit
        {
            get => _pressureUnit;
            set
            {
                if (SetField(ref _pressureUnit, value))
                {
                    _settings.PressureUnit = value;
                    OnPropertyChanged(nameof(IsHectopascal));
                    OnPropertyChanged(nameof(IsMillimeterOfMercury));
                }
            }
        }

        public WindSpeedUnit WindSpeedUnit
        {
            get => _windSpeedUnit;
            set
            {
                if (SetField(ref _windSpeedUnit, value))
                {
                    _settings.WindSpeedUnit = value;
                    OnPropertyChanged(nameof(IsMetersPerSecond));
                    OnPropertyChanged(nameof(IsKilometersPerHour));
                }
            }
        }

        public PrecipitationUnit PrecipitationUnit
        {
            get => _precipitationUnit;
            set
            {
                if (SetField(ref _precipitationUnit, value))
                {
                    _settings.PrecipitationUnit = value;
                    OnPropertyChanged(nameof(IsMillimeters));
                    OnPropertyChanged(nameof(IsInches));
                }
            }
        }

        public bool IsCelsius
        {
            get => TemperatureUnit == TemperatureUnit.Celsius;
            set => TemperatureUnit = value ? TemperatureUnit.Celsius : TemperatureUnit.Fahrenheit;
        }

        public bool IsFahrenheit
        {
            get => TemperatureUnit == TemperatureUnit.Fahrenheit;
            set => TemperatureUnit = value ? TemperatureUnit.Fahrenheit : TemperatureUnit.Celsius;
        }

        public bool IsHectopascal
        {
            get => PressureUnit == PressureUnit.Hectopascal;
            set => PressureUnit = value ? PressureUnit.Hectopascal : PressureUnit.MillimeterOfMercury;
        }

        public bool IsMillimeterOfMercury
        {
            get => PressureUnit == PressureUnit.MillimeterOfMercury;
            set => PressureUnit = value ? PressureUnit.MillimeterOfMercury : PressureUnit.Hectopascal;
        }

        public bool IsMetersPerSecond
        {
            get => WindSpeedUnit == WindSpeedUnit.MetersPerSecond;
            set => WindSpeedUnit = value ? WindSpeedUnit.MetersPerSecond : WindSpeedUnit.KilometersPerHour;
        }

        public bool IsKilometersPerHour
        {
            get => WindSpeedUnit == WindSpeedUnit.KilometersPerHour;
            set => WindSpeedUnit = value ? WindSpeedUnit.KilometersPerHour : WindSpeedUnit.MetersPerSecond;
        }

        public bool IsMillimeters
        {
            get => PrecipitationUnit == PrecipitationUnit.Millimeters;
            set => PrecipitationUnit = value ? PrecipitationUnit.Millimeters : PrecipitationUnit.Inches;
        }

        public bool IsInches
        {
            get => PrecipitationUnit == PrecipitationUnit.Inches;
            set => PrecipitationUnit = value ? PrecipitationUnit.Inches : PrecipitationUnit.Millimeters;
        }

        public SettingsViewModel()
        {
            _temperatureUnit = _settings.TemperatureUnit;
            _pressureUnit = _settings.PressureUnit;
            _windSpeedUnit = _settings.WindSpeedUnit;
            _precipitationUnit = _settings.PrecipitationUnit;
        }
    }
}