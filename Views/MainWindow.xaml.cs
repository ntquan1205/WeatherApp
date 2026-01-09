using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using WeatherApp.ViewModels;
using WeatherApp.Views;
using WeatherApp.Services;

namespace WeatherApp
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _mainViewModel;
        private readonly SettingsViewModel _settingsViewModel;
        private readonly LocationsViewModel _locationsViewModel;
        private readonly SettingsPage _settingsPage;
        private readonly LocationsPage _locationsPage;

        public MainWindow()
        {
            InitializeComponent();

            var weatherService = new OpenWeatherService();
            var geocoderService = new MockGeocoderService();
            _settingsViewModel = new SettingsViewModel();

            _mainViewModel = new MainViewModel(weatherService, _settingsViewModel);
            _locationsViewModel = new LocationsViewModel(geocoderService, _mainViewModel);

            _settingsPage = new SettingsPage { DataContext = _settingsViewModel };
            _locationsPage = new LocationsPage { DataContext = _locationsViewModel };

            DataContext = _mainViewModel;

            contentArea.Content = homeContent;
            UpdateNavigationButtons(btnHome);

            _locationsViewModel.OnNavigateBack += NavigateToHome;
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            NavigateToHome();
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            contentArea.Content = _settingsPage;
            UpdateNavigationButtons(btnSettings);
        }

        private void btnLocations_Click(object sender, RoutedEventArgs e)
        {
            contentArea.Content = _locationsPage;
            UpdateNavigationButtons(btnLocations);
        }

        private void NavigateToHome()
        {
            contentArea.Content = homeContent;
            UpdateNavigationButtons(btnHome);
        }

        private void UpdateNavigationButtons(Button activeButton)
        {
            btnHome.Background = System.Windows.Media.Brushes.Transparent;
            btnSettings.Background = System.Windows.Media.Brushes.Transparent;
            btnLocations.Background = System.Windows.Media.Brushes.Transparent;

            if (activeButton != null)
            {
                activeButton.Background = new System.Windows.Media.SolidColorBrush(
                    System.Windows.Media.Color.FromArgb(100, 3, 169, 244));
            }
        }
    }
}