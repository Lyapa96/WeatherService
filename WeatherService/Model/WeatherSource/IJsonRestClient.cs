using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WeatherService.Model.WeatherSource
{
    public interface IJsonRestClient
    {
        Task<(HttpStatusCode code, JObject content)> GetData(string url, string queryParameters);
    }
}