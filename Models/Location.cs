namespace WeatherApp.Models
{
    public class Location
    {
        public string Name { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Country { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{Name}, {Country}";
        }
    }
}