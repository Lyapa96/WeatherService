namespace WeatherService.Model.Data
{
    public class WeatherQuery
    {
        public string City { get; set; }
        public int DaysCount { get; set; } = 5;
    }
}