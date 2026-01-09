using WeatherApp.ViewModels;

namespace WeatherApp.Models
{
    public class Settings : BaseViewModel
    {
        private TemperatureUnit _temperatureUnit = TemperatureUnit.Celsius;
        public TemperatureUnit TemperatureUnit
        {
            get => _temperatureUnit;
            set => SetField(ref _temperatureUnit, value);
        }

        private PressureUnit _pressureUnit = PressureUnit.Hectopascal;
        public PressureUnit PressureUnit
        {
            get => _pressureUnit;
            set => SetField(ref _pressureUnit, value);
        }

        private WindSpeedUnit _windSpeedUnit = WindSpeedUnit.KilometersPerHour;
        public WindSpeedUnit WindSpeedUnit
        {
            get => _windSpeedUnit;
            set => SetField(ref _windSpeedUnit, value);
        }

        private PrecipitationUnit _precipitationUnit = PrecipitationUnit.Millimeters;
        public PrecipitationUnit PrecipitationUnit
        {
            get => _precipitationUnit;
            set => SetField(ref _precipitationUnit, value);
        }
    }

    public enum TemperatureUnit
    {
        Celsius,
        Fahrenheit
    }

    public enum PressureUnit
    {
        Hectopascal,
        MillimeterOfMercury
    }

    public enum WindSpeedUnit
    {
        MetersPerSecond,
        KilometersPerHour
    }

    public enum PrecipitationUnit
    {
        Millimeters,
        Inches
    }
}