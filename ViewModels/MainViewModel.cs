using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WeatherApp.Models;

namespace WeatherApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        private WeatherData _currentWeather;
        public WeatherData CurrentWeather
        {
            get => _currentWeather;
            set => SetField(ref _currentWeather, value);
        }

        private Location _selectedLocation;
        public Location SelectedLocation
        {
            get => _selectedLocation;
            set => SetField(ref _selectedLocation, value);
        }

        public ObservableCollection<Location> SavedLocations { get; set; } = new();

        public ICommand SearchCommand { get; }
        public ICommand AddLocationCommand { get; }

        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set => SetField(ref _searchText, value);
        }

        public MainViewModel()
        {
            SearchCommand = new RelayCommand(ExecuteSearch);
            AddLocationCommand = new RelayCommand(ExecuteAddLocation);

            InitializeMockData();
        }

        private void InitializeMockData()
        {
            CurrentWeather = new WeatherData
            {
                Temperature = 20.5,
                Humidity = 65,
                Pressure = 1013,
                WindSpeed = 5.2,
                WindDirection = "СЗ",
                Description = "Ясно",
                Time = DateTime.Now
            };

            SelectedLocation = new Location
            {
                Name = "Москва",
                Latitude = 55.7558,
                Longitude = 37.6173,
                Country = "Россия"
            };

            var rnd = new Random();
            for (int i = 0; i < 5; i++)
            {
                CurrentWeather.DailyForecasts.Add(new DailyForecast
                {
                    Date = DateTime.Now.AddDays(i),
                    MaxTemperature = 20 + rnd.NextDouble() * 10,
                    MinTemperature = 10 + rnd.NextDouble() * 5,
                    WeatherCode = "sunny"
                });
            }

            SavedLocations.Add(new Location { Name = "Москва", Country = "Россия" });
            SavedLocations.Add(new Location { Name = "Санкт-Петербург", Country = "Россия" });
            SavedLocations.Add(new Location { Name = "Новосибирск", Country = "Россия" });
        }

        private void ExecuteSearch(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                SelectedLocation = new Location
                {
                    Name = SearchText,
                    Country = "страна"
                };

                CurrentWeather.Temperature += 1; 
                OnPropertyChanged(nameof(CurrentWeather));
            }
        }

        private void ExecuteAddLocation(object parameter)
        {
            if (SelectedLocation != null && !SavedLocations.Any(l => l.Name == SelectedLocation.Name))
            {
                SavedLocations.Add(new Location
                {
                    Name = SelectedLocation.Name,
                    Country = SelectedLocation.Country,
                    Latitude = SelectedLocation.Latitude,
                    Longitude = SelectedLocation.Longitude
                });
            }
        }
    }
}