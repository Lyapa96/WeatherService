using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WeatherService.Model.Data;

namespace WeatherService.Model.WeatherSource.OpenWeatherData
{
    public class OpenWeatherDataSource : IWeatherSource
    {
        private const string URL = "https://samples.openweathermap.org/data/2.5/forecast";
        private readonly string apiKey;
        private readonly IJsonResponseContentParser parser;
        private readonly IJsonRestClient restClient;

        public OpenWeatherDataSource(IJsonRestClient restClient, IJsonResponseContentParser parser, string apiKey)
        {
            this.restClient = restClient;
            this.parser = parser;
            this.apiKey = apiKey;
        }

        public async Task<WeatherForecastResult> GetWeatherFor(string city, int daysCount)
        {
            var urlParameters = $"?q={city}&appid={apiKey}";
            var (statusCode, jsonResponse) = await restClient.GetData(URL, urlParameters);
            if (statusCode == HttpStatusCode.OK)
            {
                var weatherForecast = parser.Parse(jsonResponse)
                    .GroupBy(x => x.Date)
                    .Select(x => x.First())
                    .Take(daysCount)
                    .ToArray();

                return WeatherForecastResult.Successfull(weatherForecast);
            }

            return statusCode == HttpStatusCode.NotFound
                ? WeatherForecastResult.NotFound
                : WeatherForecastResult.UnknownError;
        }
    }
}