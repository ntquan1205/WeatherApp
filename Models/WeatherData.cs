using System;
using System.Collections.ObjectModel;

namespace WeatherApp.Models
{
    public class WeatherData
    {
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double Pressure { get; set; }
        public double WindSpeed { get; set; }
        public string WindDirection { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Time { get; set; }

      
        public ObservableCollection<DailyForecast> DailyForecasts { get; set; } = new();
        public ObservableCollection<HourlyForecast> HourlyForecasts { get; set; } = new();
    }

    public class DailyForecast
    {
        public DateTime Date { get; set; }
        public double MaxTemperature { get; set; }
        public double MinTemperature { get; set; }
        public string WeatherCode { get; set; } = string.Empty;
    }

    public class HourlyForecast
    {
        public DateTime Time { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double Pressure { get; set; }
    }
}