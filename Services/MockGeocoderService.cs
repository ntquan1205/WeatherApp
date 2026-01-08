using WeatherApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherApp.Services
{
    public class MockGeocoderService : IGeocoderService
    {
        private readonly List<Location> _mockLocations = new List<Location>
        {
            new Location { Name = "Moscow", Country = "Russia", Latitude = 55.7558, Longitude = 37.6173 },
            new Location { Name = "Saint Petersburg", Country = "Russia", Latitude = 59.9343, Longitude = 30.3351 },
            new Location { Name = "Vladivostok", Country = "Russia", Latitude = 43.1155, Longitude = 131.8855 },
            new Location { Name = "New York", Country = "USA", Latitude = 40.7128, Longitude = -74.0060 },
            new Location { Name = "London", Country = "UK", Latitude = 51.5074, Longitude = -0.1278 },
            new Location { Name = "Tokyo", Country = "Japan", Latitude = 35.6762, Longitude = 139.6503 }
        };

        public Task<Location> GeocodeAsync(string address)
        {
            var location = _mockLocations.Find(l =>
                l.Name.Contains(address, StringComparison.OrdinalIgnoreCase) ||
                l.Country.Contains(address, StringComparison.OrdinalIgnoreCase));

            if (location == null)
            {
                location = new Location
                {
                    Name = address,
                    Country = "Unknown",
                    Latitude = new Random().NextDouble() * 180 - 90,
                    Longitude = new Random().NextDouble() * 360 - 180
                };
            }

            return Task.FromResult(location);
        }

        public Task<List<Location>> SearchLocationsAsync(string query)
        {
            var results = _mockLocations.FindAll(l =>
                l.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                l.Country.Contains(query, StringComparison.OrdinalIgnoreCase));

            return Task.FromResult(results);
        }

        public Task<Location> ReverseGeocodeAsync(double lat, double lon)
        {
            var location = _mockLocations.Find(l =>
                Math.Abs(l.Latitude - lat) < 0.1 &&
                Math.Abs(l.Longitude - lon) < 0.1);

            if (location == null)
            {
                location = new Location
                {
                    Name = $"Location ({lat:F2}, {lon:F2})",
                    Country = "Unknown",
                    Latitude = lat,
                    Longitude = lon
                };
            }

            return Task.FromResult(location);
        }
    }
}