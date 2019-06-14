using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using WeatherService.Model.Data;

namespace WeatherService.Model.WeatherSource.OpenWeatherData
{
    public class OpenWeatherDataResponseContentParser : IJsonResponseContentParser
    {
        private const string FieldNameWithForecasts = "list";

        public IEnumerable<WeatherInfo> Parse(JObject responseContent)
        {
            var jArray = responseContent[FieldNameWithForecasts];
            return jArray.Select(x => x.ToObject<JObject>())
                .Select(ParseLine);
        }

        private WeatherInfo ParseLine(JObject item)
        {
            var date = DateTimeOffset.FromUnixTimeSeconds(item["dt"].Value<long>()).Date.ToString();
            var temp = Math.Round(ConvertToCelsius(item["main"]["temp"].Value<double>()), 1);
            var windSpeed = Math.Round(item["wind"]["speed"].Value<double>(), 1);
            var weatherType = Enum.Parse<WeatherType>(item["weather"][0]["main"].ToObject<string>());

            return new WeatherInfo
            {
                Date = date,
                Temp = temp,
                WindSpeed = windSpeed,
                WeatherType = weatherType
            };
        }

        private double ConvertToCelsius(double kelvin)
        {
            return kelvin - 273.15;
        }
    }
}