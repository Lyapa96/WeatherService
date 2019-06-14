using System.Collections.Generic;
using WeatherService.Model.Data;

namespace WeatherService.Model.Cache
{
    public interface IForecastsCache
    {
        bool TryGetForecasts(string city, out IReadOnlyList<WeatherInfo> forecasts);
        void UpdateCache(string city, IReadOnlyList<WeatherInfo> forecasts);
    }
}