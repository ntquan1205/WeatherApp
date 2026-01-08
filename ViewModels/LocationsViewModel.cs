using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.ViewModels
{
    public class LocationsViewModel : BaseViewModel
    {
        private readonly IGeocoderService _geocoderService;
        private readonly MainViewModel _mainViewModel;

        private string _searchQuery = string.Empty;
        public string SearchQuery
        {
            get => _searchQuery;
            set => SetField(ref _searchQuery, value);
        }

        public ObservableCollection<Location> SearchResults { get; set; } = new();
        public ObservableCollection<Location> FavoriteLocations { get; set; } = new();

        private Location _selectedLocation;
        public Location SelectedLocation
        {
            get => _selectedLocation;
            set => SetField(ref _selectedLocation, value);
        }

        public bool HasNoFavorites => FavoriteLocations.Count == 0;

        public ICommand SearchCommand { get; }
        public ICommand SelectLocationCommand { get; }
        public ICommand NavigateBackCommand { get; }
        public ICommand AddToFavoritesCommand { get; }
        public ICommand RemoveFromFavoritesCommand { get; }

        public LocationsViewModel(IGeocoderService geocoderService, MainViewModel mainViewModel)
        {
            _geocoderService = geocoderService;
            _mainViewModel = mainViewModel;

            SearchCommand = new RelayCommand(ExecuteSearch);
            SelectLocationCommand = new RelayCommand(ExecuteSelectLocation);
            NavigateBackCommand = new RelayCommand(ExecuteNavigateBack);
            AddToFavoritesCommand = new RelayCommand(ExecuteAddToFavorites);
            RemoveFromFavoritesCommand = new RelayCommand(ExecuteRemoveFromFavorites);

            // Initialize with some sample favorite locations
            InitializeSampleFavorites();
        }

        private void InitializeSampleFavorites()
        {
            FavoriteLocations.Add(new Location { Name = "Moscow", Country = "Russia", Latitude = 55.7558, Longitude = 37.6173 });
            FavoriteLocations.Add(new Location { Name = "London", Country = "United Kingdom", Latitude = 51.5074, Longitude = -0.1278 });
            FavoriteLocations.Add(new Location { Name = "New York", Country = "United States", Latitude = 40.7128, Longitude = -74.0060 });
            OnPropertyChanged(nameof(HasNoFavorites));
        }

        private async void ExecuteSearch(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                var results = await _geocoderService.SearchLocationsAsync(SearchQuery);
                SearchResults.Clear();
                foreach (var location in results)
                {
                    SearchResults.Add(location);
                }
            }
        }

        private void ExecuteSelectLocation(object parameter)
        {
            if (SelectedLocation != null)
            {
                _mainViewModel.LocationName = $"{SelectedLocation.Name}, {SelectedLocation.Country}";
                ExecuteNavigateBack(parameter);
            }
        }

        private void ExecuteAddToFavorites(object parameter)
        {
            if (parameter is Location location && !FavoriteLocations.Contains(location))
            {
                FavoriteLocations.Add(location);
                OnPropertyChanged(nameof(HasNoFavorites));
            }
        }

        private void ExecuteRemoveFromFavorites(object parameter)
        {
            if (parameter is Location location)
            {
                FavoriteLocations.Remove(location);
                OnPropertyChanged(nameof(HasNoFavorites));
            }
        }

        private void ExecuteNavigateBack(object parameter)
        {
            OnNavigateBack?.Invoke();
        }

        public event System.Action OnNavigateBack;
    }
}