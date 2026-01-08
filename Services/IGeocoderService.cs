using WeatherApp.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WeatherApp.Services
{
    public interface IGeocoderService
    {
        Task<Location> GeocodeAsync(string address);
        Task<List<Location>> SearchLocationsAsync(string query);
        Task<Location> ReverseGeocodeAsync(double lat, double lon);
    }
}