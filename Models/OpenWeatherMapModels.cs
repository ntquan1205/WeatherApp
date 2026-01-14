using System.Collections.Generic;

namespace WeatherApp.Models.Api
{
    public class GeoResponse
    {
        public string name { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string country { get; set; }
    }

    public class WeatherResponse
    {
        public List<WeatherDescription> weather { get; set; }
        public MainData main { get; set; }
        public WindData wind { get; set; }
        public SysData sys { get; set; }
        public long dt { get; set; }
        public int visibility { get; set; }

        public int timezone { get; set; }
    }

    public class WeatherDescription
    {
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class MainData
    {
        public double temp { get; set; }
        public double pressure { get; set; }
        public double humidity { get; set; }
    }

    public class WindData
    {
        public double speed { get; set; }
        public int deg { get; set; }
    }

    public class SysData
    {
        public long sunrise { get; set; }
        public long sunset { get; set; }
        public string country { get; set; }
    }
    public class ForecastResponse
    {
        public List<ForecastItem> list { get; set; }
    }

    public class ForecastItem
    {
        public long dt { get; set; }
        public MainData main { get; set; }
        public List<WeatherDescription> weather { get; set; }
        public string dt_txt { get; set; }
    }
}