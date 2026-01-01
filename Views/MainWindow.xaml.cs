using System.Windows;
using WeatherApp.ViewModels;

namespace WeatherApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}