using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WeatherService.Model.WeatherSource.OpenWeatherData
{
    public class OpenWeatherDataJsonRestClient : IJsonRestClient
    {
        public async Task<(HttpStatusCode code, JObject content)> GetData(string url, string queryParameters)
        {
            var response = await SendRequest(url, queryParameters);
            var jsonContent = await response.Content.ReadAsAsync<JObject>();
            return (response.StatusCode, jsonContent);
        }

        private static async Task<HttpResponseMessage> SendRequest(string url, string query)
        {
            var client = new HttpClient {BaseAddress = new Uri(url)};
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var urlParameters = query;

            var response = await client.GetAsync(urlParameters);
            return response;
        }
    }
}