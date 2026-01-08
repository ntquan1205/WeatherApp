using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeatherApp.Models;

namespace WeatherApp.Views
{
    /// <summary>
    /// Interaction logic for LocationsPage.xaml
    /// </summary>
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

        private void FavoriteLocation_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is Location location)
            {
                var viewModel = DataContext as ViewModels.LocationsViewModel;
                viewModel?.SelectLocationCommand.Execute(location);
            }
        }

        private void RemoveFavorite_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Location location)
            {
                var viewModel = DataContext as ViewModels.LocationsViewModel;
                viewModel?.RemoveFromFavoritesCommand.Execute(location);
                e.Handled = true;
            }
        }
    }
}
