using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WeatherApp.Views
{
    public partial class LocationsPage : UserControl
    {
        public LocationsPage()
        {
            InitializeComponent();
        }

        private void txtSearchLocation_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearchLocation.Text) && txtSearchLocation.Text.Length > 0)
                textSearchHint.Visibility = Visibility.Collapsed;
            else
                textSearchHint.Visibility = Visibility.Visible;
        }

        private void SearchResult_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is Models.Location location)
            {
                var viewModel = DataContext as ViewModels.LocationsViewModel;
                viewModel?.SelectLocationCommand.Execute(location);
            }
        }
    }
}