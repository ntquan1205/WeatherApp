using WeatherApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherApp.Services
{
    public class MockWeatherService : IWeatherService
    {
        private readonly Random _random = new Random();

        public Task<WeatherData> GetCurrentWeatherAsync(string location)
        {
            var weather = new WeatherData
            {
                Temperature = _random.Next(-10, 35),
                Humidity = _random.Next(30, 90),
                Pressure = _random.Next(980, 1030),
                WindSpeed = _random.Next(0, 15) + _random.NextDouble(),
                WindDirection = GetRandomDirection(),
                Description = GetRandomDescription(),
                Time = DateTime.Now
            };

            return Task.FromResult(weather);
        }

        public Task<List<DailyForecast>> GetWeeklyForecastAsync(string location)
        {
            var forecasts = new List<DailyForecast>();
            var days = new[] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
            var descriptions = new[] { "Sunny", "Cloudy", "Rainy", "Snowy", "Stormy" };

            for (int i = 0; i < 7; i++)
            {
                forecasts.Add(new DailyForecast
                {
                    DayOfWeek = days[i],
                    MaxTemperature = _random.Next(15, 30),
                    MinTemperature = _random.Next(5, 15),
                    WeatherIcon = GetRandomIcon(),
                    Date = DateTime.Now.AddDays(i),
                });
            }

            return Task.FromResult(forecasts);
        }

        public Task<WeatherData> GetCurrentWeatherByCoordinatesAsync(double lat, double lon)
        {
            return GetCurrentWeatherAsync($"Lat: {lat}, Lon: {lon}");
        }

        public Task<List<DailyForecast>> GetWeeklyForecastByCoordinatesAsync(double lat, double lon)
        {
            return GetWeeklyForecastAsync($"Lat: {lat}, Lon: {lon}");
        }

        private string GetRandomDirection()
        {
            var directions = new[] { "N", "NE", "E", "SE", "S", "SW", "W", "NW" };
            return directions[_random.Next(directions.Length)];
        }

        private string GetRandomDescription()
        {
            var descriptions = new[] { "Sunny", "Partly Cloudy", "Cloudy", "Rainy", "Snowy" };
            return descriptions[_random.Next(descriptions.Length)];
        }

        private string GetRandomIcon()
        {
            var icons = new[]
            {
                "/WeatherApp;component/Images/sun.png",
                "/WeatherApp;component/Images/sun_cloud.png",
                "/WeatherApp;component/Images/cloud.png",
                "/WeatherApp;component/Images/rain.png",
                "/WeatherApp;component/Images/snow.png"
            };
            return icons[_random.Next(icons.Length)];
        }
    }
}