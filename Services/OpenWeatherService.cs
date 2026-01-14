using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.Models.Api;

namespace WeatherApp.Services
{
    public class OpenWeatherService : IWeatherService, IGeocoderService
    {
        private const string API_KEY = "a877ea4d66f2aff2e1e680d721103980"; 
        private readonly HttpClient _httpClient;

        public OpenWeatherService()
        {
            _httpClient = new HttpClient();
        }


        public async Task<List<Location>> SearchLocationsAsync(string query)
        {
            string url = $"http://api.openweathermap.org/geo/1.0/direct?q={query}&limit=5&appid={API_KEY}";

            try
            {
                var json = await _httpClient.GetStringAsync(url);
                var geoResults = JsonConvert.DeserializeObject<List<GeoResponse>>(json);

                return geoResults.Select(g => new Location
                {
                    Name = g.name,
                    Country = g.country,
                    Latitude = g.lat,
                    Longitude = g.lon
                }).ToList();
            }
            catch
            {
                return new List<Location>();
            }
        }

        public async Task<Location> GeocodeAsync(string address)
        {
            var results = await SearchLocationsAsync(address);
            return results.FirstOrDefault();
        }

        public Task<Location> ReverseGeocodeAsync(double lat, double lon)
        {
            return Task.FromResult(new Location { Name = "Current Location", Latitude = lat, Longitude = lon });
        }

        private DateTime ConvertUnixToDateTime(long unixTime, int timezoneOffset)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            return dtDateTime.AddSeconds(unixTime + timezoneOffset);
        }
        public async Task<WeatherData> GetCurrentWeatherByCoordinatesAsync(double lat, double lon)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&units=metric&appid={API_KEY}";

            var json = await _httpClient.GetStringAsync(url);
            var info = JsonConvert.DeserializeObject<WeatherResponse>(json);

            return new WeatherData
            {
                Temperature = info.main.temp,
                Humidity = info.main.humidity,
                Pressure = info.main.pressure,
                WindSpeed = info.wind.speed, 
                Description = info.weather[0].description,
                Time = ConvertUnixToDateTime(info.dt, info.timezone),
                Sunrise = ConvertUnixToDateTime(info.sys.sunrise, info.timezone),
                Sunset = ConvertUnixToDateTime(info.sys.sunset, info.timezone),
                Visibility = info.visibility / 1000.0, 
                Precipitation = 0 
            };
        }

        public async Task<List<DailyForecast>> GetWeeklyForecastByCoordinatesAsync(double lat, double lon)
        {
            string url = $"https://api.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lon}&units=metric&appid={API_KEY}";

            var json = await _httpClient.GetStringAsync(url);
            var forecastData = JsonConvert.DeserializeObject<ForecastResponse>(json);

            var dailyForecasts = new List<DailyForecast>();

            var noonForecasts = forecastData.list
                .Where(x => DateTime.Parse(x.dt_txt).Hour == 12)
                .Take(5); 

            foreach (var item in noonForecasts)
            {
                var date = ConvertUnixToDateTime(item.dt);
                dailyForecasts.Add(new DailyForecast
                {
                    Date = date,
                    DayOfWeek = date.DayOfWeek.ToString(),
                    MaxTemperature = item.main.temp, 
                    MinTemperature = item.main.temp - 2, 
                    WeatherIcon = $"http://openweathermap.org/img/w/{item.weather[0].icon}.png" 
                });
            }

            return dailyForecasts;
        }

        public Task<WeatherData> GetCurrentWeatherAsync(string location) => throw new NotImplementedException("Use Coordinates version");
        public Task<List<DailyForecast>> GetWeeklyForecastAsync(string location) => throw new NotImplementedException("Use Coordinates version");

        private DateTime ConvertUnixToDateTime(long unixTime)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            return dtDateTime.AddSeconds(unixTime).ToLocalTime();
        }
    }
}