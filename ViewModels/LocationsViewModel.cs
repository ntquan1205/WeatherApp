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

        private bool _isSearching = false;
        public bool IsSearching
        {
            get => _isSearching;
            set => SetField(ref _isSearching, value);
        }

        public ObservableCollection<Location> SearchResults { get; set; } = new();

        public bool HasSearchResults => SearchResults.Any();

        private Location _selectedLocation;
        public Location SelectedLocation
        {
            get => _selectedLocation;
            set => SetField(ref _selectedLocation, value);
        }

        public ICommand SearchCommand { get; }
        public ICommand SelectLocationCommand { get; }

        public LocationsViewModel(IGeocoderService geocoderService, MainViewModel mainViewModel)
        {
            _geocoderService = geocoderService;
            _mainViewModel = mainViewModel;

            SearchCommand = new RelayCommand(async (param) => await ExecuteSearch(param));
            SelectLocationCommand = new RelayCommand(ExecuteSelectLocation);
        }

        private async Task ExecuteSearch(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                IsSearching = true;
                OnPropertyChanged(nameof(HasSearchResults));

                try
                {
                    var results = await _geocoderService.SearchLocationsAsync(SearchQuery);
                    SearchResults.Clear();
                    foreach (var location in results)
                    {
                        SearchResults.Add(location);
                    }
                }
                finally
                {
                    IsSearching = false;
                    OnPropertyChanged(nameof(HasSearchResults));
                }
            }
        }

        private void ExecuteSelectLocation(object parameter)
        {
            if (SelectedLocation != null)
            {
                _mainViewModel.LocationName = $"{SelectedLocation.Name}, {SelectedLocation.Country}";
                _mainViewModel.LoadWeatherForLocation(SelectedLocation);
                OnNavigateBack?.Invoke();
            }
        }

        public event System.Action OnNavigateBack;
    }
}