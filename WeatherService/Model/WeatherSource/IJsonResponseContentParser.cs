using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using WeatherService.Model.Data;

namespace WeatherService.Model.WeatherSource
{
    public interface IJsonResponseContentParser
    {
        IEnumerable<WeatherInfo> Parse(JObject responseContent);
    }
}