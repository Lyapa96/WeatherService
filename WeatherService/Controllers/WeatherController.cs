using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeatherService.Model.Cache;
using WeatherService.Model.Data;
using WeatherService.Model.WeatherSource;

namespace WeatherService.Controllers
{
    [Route("api/[controller]")]
    public class WeatherController : Controller
    {
        private readonly IWeatherSource weatherSource;
        private readonly IForecastsCache cache;

        public WeatherController(
            IWeatherSource weatherSource,
            IForecastsCache cache)
        {
            this.weatherSource = weatherSource ?? throw new ArgumentNullException($"{nameof(weatherSource)}");
            this.cache = cache ?? throw new ArgumentNullException($"{nameof(cache)}");
        }

        [HttpGet("[action]")]
        public async Task<WeatherForecastResult> WeatherForecasts([FromQuery] WeatherQuery weatherQuery)
        {
            if (cache.TryGetForecasts(weatherQuery.City, out var cachedForecasts))
                return WeatherForecastResult.Successfull(cachedForecasts);

            var weatherForecastResult = await weatherSource.GetWeatherFor(weatherQuery.City, weatherQuery.DaysCount);

            if (weatherForecastResult.Status == Status.Success)
                cache.UpdateCache(weatherQuery.City, weatherForecastResult.WeatherForecast);

            return weatherForecastResult;
        }
    }
}