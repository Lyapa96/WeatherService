using System.Collections.Generic;
using WeatherService.Model.Data;

namespace WeatherService.Model.Cache
{
    public class DummyForecastsCache : IForecastsCache
    {
        private readonly Dictionary<string, IReadOnlyList<WeatherInfo>> cache;

        public DummyForecastsCache()
        {
            cache = new Dictionary<string, IReadOnlyList<WeatherInfo>>();
        }
        public bool TryGetForecasts(string city, out IReadOnlyList<WeatherInfo> forecasts)
        {
            forecasts = null;
            if (!cache.ContainsKey(city))
                return false;

            forecasts = cache[city];
            return true;
        }

        public void UpdateCache(string city, IReadOnlyList<WeatherInfo> forecasts)
        {
            cache[city] = forecasts;
        }
    }
}