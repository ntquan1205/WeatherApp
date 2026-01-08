using WeatherApp.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WeatherApp.Services
{
    public interface IWeatherService
    {
        Task<WeatherData> GetCurrentWeatherAsync(string location);
        Task<List<DailyForecast>> GetWeeklyForecastAsync(string location);
        Task<WeatherData> GetCurrentWeatherByCoordinatesAsync(double lat, double lon);
        Task<List<DailyForecast>> GetWeeklyForecastByCoordinatesAsync(double lat, double lon);
    }
}
