using System.Threading.Tasks;
using WeatherService.Model.Data;

namespace WeatherService.Model.WeatherSource
{
    public interface IWeatherSource
    {
        Task<WeatherForecastResult> GetWeatherFor(string city, int daysCount);
    }
}