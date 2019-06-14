using System.Collections.Generic;

namespace WeatherService.Model.Data
{
    public class WeatherForecastResult
    {
        public static WeatherForecastResult NotFound => new WeatherForecastResult {Status = Status.NotFoundDataByCity};
        public static WeatherForecastResult UnknownError => new WeatherForecastResult{Status = Status.UncknownError};
        public static WeatherForecastResult Successfull(IReadOnlyList<WeatherInfo> weatherForecast) => new WeatherForecastResult
        {
            Status = Status.Success, 
            WeatherForecast = weatherForecast
        };
        
        public Status Status;
        public IReadOnlyList<WeatherInfo> WeatherForecast { get; set; }
    }
}