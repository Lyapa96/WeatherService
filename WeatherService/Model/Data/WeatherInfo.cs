namespace WeatherService.Model.Data
{
    public class WeatherInfo
    {
        public string Date { get; set; }
        public double WindSpeed { get; set; }
        public double Temp { get; set; }
        public WeatherType WeatherType { get; set; }
    }
}