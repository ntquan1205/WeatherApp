using System;
using System.ComponentModel;

namespace WeatherApp.Models
{
    public class WeatherData : INotifyPropertyChanged
    {
        private double _temperature;
        private double _humidity;
        private double _pressure;
        private double _windSpeed;
        private string _windDirection = string.Empty;
        private string _description = string.Empty;
        private DateTime _time;
        private double _precipitation;
        private DateTime _sunrise;
        private DateTime _sunset;
        private double _visibility;
        private int _airQuality;
        private string _airQualityStatus;

        public double Precipitation
        {
            get => _precipitation;
            set
            {
                _precipitation = value;
                OnPropertyChanged(nameof(Precipitation));
            }
        }
        public DateTime Sunrise
        {
            get => _sunrise;
            set
            {
                _sunrise = value;
                OnPropertyChanged(nameof(Sunrise));
            }
        }
        public DateTime Sunset
        {
            get => _sunset;
            set
            {
                _sunset = value;
                OnPropertyChanged(nameof(Sunset));
            }
        }
        public double Visibility
        {
            get => _visibility;
            set
            {
                _visibility = value;
                OnPropertyChanged(nameof(Visibility));  
            }
        }
        public int AirQuality
        {
            get => _airQuality;
            set
            {
                _airQuality = value;
                OnPropertyChanged(nameof(AirQuality));
            }
        }
        public string AirQualityStatus
        {
            get => _airQualityStatus;
            set
            {
                _airQualityStatus= value;
                OnPropertyChanged(nameof(AirQualityStatus));
            }
        }

        public double Temperature
        {
            get => _temperature;
            set
            {
                _temperature = value;
                OnPropertyChanged(nameof(Temperature));
            }
        }

        public double Humidity
        {
            get => _humidity;
            set
            {
                _humidity = value;
                OnPropertyChanged(nameof(Humidity));
            }
        }

        public double Pressure
        {
            get => _pressure;
            set
            {
                _pressure = value;
                OnPropertyChanged(nameof(Pressure));
            }
        }

        public double WindSpeed
        {
            get => _windSpeed;
            set
            {
                _windSpeed = value;
                OnPropertyChanged(nameof(WindSpeed));
            }
        }

        public string WindDirection
        {
            get => _windDirection;
            set
            {
                _windDirection = value;
                OnPropertyChanged(nameof(WindDirection));
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public DateTime Time
        {
            get => _time;
            set
            {
                _time = value;
                OnPropertyChanged(nameof(Time));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class DailyForecast : INotifyPropertyChanged
    {
        private string _dayOfWeek = string.Empty;
        private double _maxTemperature;
        private double _minTemperature;
        private string _weatherIcon = string.Empty;
        private DateTime _date;

        public string DayOfWeek
        {
            get => _dayOfWeek;
            set
            {
                _dayOfWeek = value;
                OnPropertyChanged(nameof(DayOfWeek));
            }
        }

        public double MaxTemperature
        {
            get => _maxTemperature;
            set
            {
                _maxTemperature = value;
                OnPropertyChanged(nameof(MaxTemperature));
            }
        }

        public double MinTemperature
        {
            get => _minTemperature;
            set
            {
                _minTemperature = value;
                OnPropertyChanged(nameof(MinTemperature));
            }
        }

        public string WeatherIcon
        {
            get => _weatherIcon;
            set
            {
                _weatherIcon = value;
                OnPropertyChanged(nameof(WeatherIcon));
            }
        }

        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}