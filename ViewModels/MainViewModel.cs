using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WeatherApp.Models;

namespace WeatherApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
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
                if (_currentWeather != null)
                {
                    _currentWeather.PropertyChanged -= CurrentWeather_PropertyChanged;
                }

                if (SetField(ref _currentWeather, value))
                {
                    if (_currentWeather != null)
                    {
                        _currentWeather.PropertyChanged += CurrentWeather_PropertyChanged;
                    }
                    OnPropertyChanged(nameof(FormattedTime));
                }
            }
        }

        private void CurrentWeather_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(WeatherData.Time))
            {
                OnPropertyChanged(nameof(FormattedTime));
            }
        }


        public ObservableCollection<DailyForecast> DailyForecasts { get; set; } = new();

        private string _locationName = "Vladvostok, Russia";
        public string LocationName
        {
            get => _locationName;
            set => SetField(ref _locationName, value);
        }

        private int _rainProbability = 30;
        public int RainProbability
        {
            get => _rainProbability;
            set => SetField(ref _rainProbability, value);
        }

        private double _uvIndex1 = 4;
        public double UVIndex1
        {
            get => _uvIndex1;
            set => SetField(ref _uvIndex1, value);
        }

        private double _uvIndex2 = 7;
        public double UVIndex2
        {
            get => _uvIndex2;
            set => SetField(ref _uvIndex2, value);
        }

        private double _uvIndex3 = 3;
        public double UVIndex3
        {
            get => _uvIndex3;
            set => SetField(ref _uvIndex3, value);
        }

        private string _sunriseTime = "6:35 AM";
        public string SunriseTime
        {
            get => _sunriseTime;
            set => SetField(ref _sunriseTime, value);
        }

        private string _sunriseChange = "-1m 46s";
        public string SunriseChange
        {
            get => _sunriseChange;
            set => SetField(ref _sunriseChange, value);
        }

        private string _sunsetTime = "5:12 AM";
        public string SunsetTime
        {
            get => _sunsetTime;
            set => SetField(ref _sunsetTime, value);
        }

        private string _sunsetChange = "+2m 15s";
        public string SunsetChange
        {
            get => _sunsetChange;
            set => SetField(ref _sunsetChange, value);
        }

        private double _visibility = 5.2;
        public double Visibility
        {
            get => _visibility;
            set => SetField(ref _visibility, value);
        }

        private string _visibilityStatus = "Average";
        public string VisibilityStatus
        {
            get => _visibilityStatus;
            set => SetField(ref _visibilityStatus, value);
        }

   
        private int _airQuality = 105;
        public int AirQuality
        {
            get => _airQuality;
            set => SetField(ref _airQuality, value);
        }

        private string _airQualityStatus = "Unhealthy";
        public string AirQualityStatus
        {
            get => _airQualityStatus;
            set => SetField(ref _airQualityStatus, value);
        }

        private string _weatherIcon = "/WeatherApp;component/Images/sun_cloud.png";
        public string WeatherIcon
        {
            get => _weatherIcon;
            set => SetField(ref _weatherIcon, value);
        }

        public string FormattedTime
        {
            get
            {
                if (CurrentWeather?.Time != null)
                {
                    return $"{CurrentWeather.Time:dddd}, {CurrentWeather.Time:HH:mm}";
                }
                return DateTime.Now.ToString("dddd, HH:mm");
            }
        }

        public ICommand SearchCommand { get; }
        public ICommand UpdateCommand { get; }

        #endregion

        public MainViewModel()
        {
            SearchCommand = new RelayCommand(ExecuteSearch);
            UpdateCommand = new RelayCommand(ExecuteUpdate);

            InitializeMockData();
        }

        private void InitializeMockData()
        {
            CurrentWeather = new WeatherData
            {
                Temperature = 12,
                Humidity = 56,
                Pressure = 1013,
                WindSpeed = 7.7,
                WindDirection = "WSW",
                Description = "Mostly Cloudy",
                Time = DateTime.Now
            };

            WeatherIcon = "/WeatherApp;component/Images/sun_cloud.png";

            RainProbability = 30;
            UVIndex1 = 4;
            UVIndex2 = 7;
            UVIndex3 = 3;
            SunriseTime = "6:35 AM";
            SunriseChange = "-1m 46s";
            SunsetTime = "5:12 AM";
            SunsetChange = "+2m 15s";
            Visibility = 5.2;
            VisibilityStatus = "Average";
            AirQuality = 105;
            AirQualityStatus = "Unhealthy";
            LocationName = "Vladvostok, Russia";

            DailyForecasts.Clear();
            var days = new[] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
            var maxTemps = new[] { 12, 1, 1, 7, 14, 26, 3 };
            var minTemps = new[] { 3, 6, -10, -1, 3, 10, -3 };
            var icons = new[]
            {
                "/WeatherApp;component/Images/sun.png",
                "/WeatherApp;component/Images/sun_cloud.png",
                "/WeatherApp;component/Images/snow.png",
                "/WeatherApp;component/Images/rain.png",
                "/WeatherApp;component/Images/rain_cloud.png",
                "/WeatherApp;component/Images/sun.png",
                "/WeatherApp;component/Images/storm.png"
            };

            for (int i = 0; i < 7; i++)
            {
                DailyForecasts.Add(new DailyForecast
                {
                    DayOfWeek = days[i],
                    MaxTemperature = maxTemps[i],
                    MinTemperature = minTemps[i],
                    WeatherIcon = icons[i],
                    Date = DateTime.Now.AddDays(i)
                });
            }
        }

        private void ExecuteSearch(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                var random = new Random();
                CurrentWeather.Temperature = random.Next(-5, 25);
                CurrentWeather.Humidity = random.Next(30, 90);
                CurrentWeather.WindSpeed = random.Next(0, 15) + random.NextDouble();


                for (int i = 0; i < DailyForecasts.Count; i++)
                {
                    DailyForecasts[i].MaxTemperature = random.Next(0, 30);
                    DailyForecasts[i].MinTemperature = random.Next(-10, 15);
                }

                OnPropertyChanged(nameof(CurrentWeather));
                OnPropertyChanged(nameof(DailyForecasts));
            }
        }

        private void ExecuteUpdate(object parameter)
        {
            InitializeMockData();
        }
    }
}