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

            return forecastData.list.GroupBy(x => DateTime.Parse(x.dt_txt).Date).Select(group => new DailyForecast
            {
                Date = group.Key,
                DayOfWeek = group.Key.DayOfWeek.ToString(),
                MaxTemperature = group.Max(x => x.main.temp_max),
                MinTemperature = group.Min(x => x.main.temp_min),
                WeatherIcon = $"http://openweathermap.org/img/w/{group.First().weather[0].icon}.png"
            })
            .Take(5)
            .ToList();
        }

        public Task<WeatherData> GetCurrentWeatherAsync(string location) => throw new NotImplementedException("Use Coordinates version");
        public Task<List<DailyForecast>> GetWeeklyForecastAsync(string location) => throw new NotImplementedException("Use Coordinates version");
    }
}