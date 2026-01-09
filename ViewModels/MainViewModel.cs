using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IWeatherService _weatherService;
        private readonly SettingsViewModel _settingsViewModel;

        #region Properties

        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set => SetField(ref _searchText, value);
        }

        private WeatherData _currentWeather;
        public WeatherData CurrentWeather
        {
            get => _currentWeather;
            set
            {
                if (SetField(ref _currentWeather, value))
                {
                    OnPropertyChanged(nameof(DisplayTemperature));
                    OnPropertyChanged(nameof(DisplayPrecipitation));
                    OnPropertyChanged(nameof(DisplayVisibility));
                    OnPropertyChanged(nameof(DisplaySunrise));
                    OnPropertyChanged(nameof(DisplaySunset));
                }
            }
        }

        public ObservableCollection<DailyForecast> DailyForecasts { get; set; } = new();

        private string _locationName = "Hanoi, Vietnam";
        public string LocationName
        {
            get => _locationName;
            set => SetField(ref _locationName, value);
        }

        public string DisplayPrecipitation
        {
            get
            {
                if (CurrentWeather == null) return "0 mm";

                return _settingsViewModel.IsMillimeters
                    ? $"{CurrentWeather.Precipitation:F1} mm"
                    : $"{MmToInches(CurrentWeather.Precipitation):F2} \""; 
            }
        }

        public string DisplayVisibility
        {
            get
            {
                if (CurrentWeather == null) return "0 km";
                return $"{CurrentWeather.Visibility:F1} km";
            }
        }

        public string DisplaySunrise => CurrentWeather?.Sunrise.ToString("HH:mm") ?? "--:--";
        public string DisplaySunset => CurrentWeather?.Sunset.ToString("HH:mm") ?? "--:--";
        public string DisplayTemperature
        {
            get
            {
                if (CurrentWeather == null) return "0°C";

                return _settingsViewModel.TemperatureUnit == TemperatureUnit.Celsius
                    ? $"{CurrentWeather.Temperature:F1}°C"
                    : $"{CelsiusToFahrenheit(CurrentWeather.Temperature):F1}°F";
            }
        }

        public string DisplayWindSpeed
        {
            get
            {
                if (CurrentWeather == null) return "0 km/h";

                return _settingsViewModel.WindSpeedUnit == WindSpeedUnit.KilometersPerHour
                    ? $"{CurrentWeather.WindSpeed:F1} km/h"
                    : $"{KmhToMs(CurrentWeather.WindSpeed):F1} m/s";
            }
        }

        public string DisplayPressure
        {
            get
            {
                if (CurrentWeather == null) return "0 hPa";

                return _settingsViewModel.PressureUnit == PressureUnit.Hectopascal
                    ? $"{CurrentWeather.Pressure:F0} hPa"
                    : $"{HpaToMmHg(CurrentWeather.Pressure):F1} mmHg";
            }
        }

        public string FormattedTime => CurrentWeather?.Time.ToString("dddd, HH:mm", System.Globalization.CultureInfo.InvariantCulture)
                               ?? DateTime.Now.ToString("dddd, HH:mm", System.Globalization.CultureInfo.InvariantCulture);

        public ICommand SearchCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand NavigateToSettingsCommand { get; }
        public ICommand NavigateToLocationsCommand { get; }

        #endregion

        public MainViewModel(IWeatherService weatherService, SettingsViewModel settingsViewModel)
        {
            _weatherService = weatherService; 
            _settingsViewModel = settingsViewModel;
            _settingsViewModel.PropertyChanged += Settings_PropertyChanged;

            SearchCommand = new RelayCommand(ExecuteSearch);
            UpdateCommand = new RelayCommand(ExecuteUpdate);
            NavigateToSettingsCommand = new RelayCommand(NavigateToSettings);
            NavigateToLocationsCommand = new RelayCommand(NavigateToLocations);

            LoadDefaultWeather();
        }

        private async void LoadDefaultWeather()
        {
  
            var weatherService = _weatherService as OpenWeatherService;


            double hanoiLat = 21.0285;
            double hanoiLon = 105.8542;

            if (_weatherService != null)
            {
                CurrentWeather = await _weatherService.GetCurrentWeatherByCoordinatesAsync(hanoiLat, hanoiLon);
                var forecasts = await _weatherService.GetWeeklyForecastByCoordinatesAsync(hanoiLat, hanoiLon);

                DailyForecasts.Clear();
                foreach (var item in forecasts)
                {
                    // THÊM DÒNG NÀY: Set đơn vị ngay khi tạo đối tượng
                    item.SetTemperatureUnit(_settingsViewModel.TemperatureUnit);

                    DailyForecasts.Add(item);
                }
            }
        }


        private async void ExecuteSearch(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                var geocoder = new OpenWeatherService();
                var location = await geocoder.GeocodeAsync(SearchText);

                if (location != null)
                {
                    LocationName = $"{location.Name}, {location.Country}";

                    CurrentWeather = await _weatherService.GetCurrentWeatherByCoordinatesAsync(
                        location.Latitude, location.Longitude);

                    var forecasts = await _weatherService.GetWeeklyForecastByCoordinatesAsync(
                        location.Latitude, location.Longitude);

                    DailyForecasts.Clear();
                    foreach (var forecast in forecasts)
                    {
                        DailyForecasts.Add(forecast);
                    }
                }
            }
        }

        private void Settings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(DisplayTemperature));
            OnPropertyChanged(nameof(DisplayWindSpeed));
            OnPropertyChanged(nameof(DisplayPressure));

            if (DailyForecasts != null)
            {
                foreach (var day in DailyForecasts)
                {
                    day.SetTemperatureUnit(_settingsViewModel.TemperatureUnit);
                }
            }
        }


        private async void ExecuteUpdate(object parameter)
        {
            var geocoder = new MockGeocoderService();
            var location = await geocoder.GeocodeAsync(LocationName.Split(',')[0]);

            CurrentWeather = await _weatherService.GetCurrentWeatherByCoordinatesAsync(
                location.Latitude, location.Longitude);

            var forecasts = await _weatherService.GetWeeklyForecastByCoordinatesAsync(
                location.Latitude, location.Longitude);

            DailyForecasts.Clear();
            foreach (var forecast in forecasts)
            {
                DailyForecasts.Add(forecast);
            }
        }

        private void NavigateToSettings(object parameter)
        {
            OnNavigateToSettings?.Invoke();
        }

        private void NavigateToLocations(object parameter)
        {
            OnNavigateToLocations?.Invoke();
        }

        public async Task LoadWeatherForLocation(Location location)
        {
            if (location != null)
            {
                LocationName = $"{location.Name}, {location.Country}";

                CurrentWeather = await _weatherService.GetCurrentWeatherByCoordinatesAsync(
                    location.Latitude, location.Longitude);

                var forecasts = await _weatherService.GetWeeklyForecastByCoordinatesAsync(
                    location.Latitude, location.Longitude);

                DailyForecasts.Clear();
                foreach (var forecast in forecasts)
                {
                    DailyForecasts.Add(forecast);
                }
                OnPropertyChanged(nameof(DailyForecasts));
            }
        }

        private double CelsiusToFahrenheit(double celsius) => celsius * 9 / 5 + 32;
        private double KmhToMs(double kmh) => kmh / 3.6;
        private double HpaToMmHg(double hpa) => hpa * 0.750062;

        private double MmToInches(double mm) => mm / 25.4;

        public event Action OnNavigateToSettings;
        public event Action OnNavigateToLocations;

    }
}