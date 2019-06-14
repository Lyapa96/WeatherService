using System;
using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using WeatherService.Controllers;
using WeatherService.Model.Cache;
using WeatherService.Model.Data;
using WeatherService.Model.WeatherSource;

namespace WeatherService.Tests
{
    public class WeatherControllerTests
    {
        public class Ctor
        {
            private IForecastsCache cache;
            private IWeatherSource weatherSource;

            [SetUp]
            public void SetUp()
            {
                cache = Substitute.For<IForecastsCache>();
                weatherSource = Substitute.For<IWeatherSource>();
            }

            [Test]
            public void Should_throw_exception_when_cache_is_null()
            {
                Action action = () => new WeatherController(weatherSource, null);

                action.Should().Throw<ArgumentNullException>();
            }

            [Test]
            public void Should_throw_exception_when_weather_source_is_null()
            {
                Action action = () => new WeatherController(null, cache);

                action.Should().Throw<ArgumentNullException>();
            }

            [Test]
            public void Should_create_controller()
            {
                Action action = () => new WeatherController(weatherSource, cache);

                action.Should().NotThrow();
            }
        }

        public class WeatherForecasts
        {
            private const string DefaultCity = "TestCity";
            private IForecastsCache cache;
            private WeatherQuery query;
            private WeatherController watherController;
            private IWeatherSource weatherSource;

            [SetUp]
            public void SetUp()
            {
                cache = Substitute.For<IForecastsCache>();
                cache.TryGetForecasts(DefaultCity, out _).Returns(false);
                weatherSource = Substitute.For<IWeatherSource>();
                weatherSource.GetWeatherFor(Arg.Any<string>(), Arg.Any<int>())
                    .Returns(new WeatherForecastResult());
                watherController = new WeatherController(weatherSource, cache);

                query = new WeatherQuery
                {
                    City = DefaultCity,
                    DaysCount = 10
                };
            }

            [Test]
            public void Should_return_weather_forecasts_form_weather_source_when_cache_does_not_contain_city()
            {
                var result = new WeatherForecastResult
                {
                    Status = Status.Success,
                    WeatherForecast = new List<WeatherInfo>()
                };
                weatherSource.GetWeatherFor(query.City, query.DaysCount)
                    .Returns(result);

                var weatherForecastResult = watherController.WeatherForecasts(query).Result;

                weatherForecastResult.Should().BeEquivalentTo(result);
            }

            [Test]
            public void Should_update_cache_when_response_successful()
            {
                var forecasts = new[] {new WeatherInfo()};
                var result = new WeatherForecastResult
                {
                    Status = Status.Success,
                    WeatherForecast = forecasts
                };
                weatherSource.GetWeatherFor(query.City, query.DaysCount)
                    .Returns(result);

                var _ = watherController.WeatherForecasts(query).Result;

                cache.Received().UpdateCache(query.City, forecasts);
            }

            [Test]
            public void Should_not_update_cache_when_response_unsuccessful()
            {
                var forecasts = new[] {new WeatherInfo()};
                var result = new WeatherForecastResult
                {
                    Status = Status.UncknownError,
                    WeatherForecast = forecasts
                };
                weatherSource.GetWeatherFor(query.City, query.DaysCount)
                    .Returns(result);

                var _ = watherController.WeatherForecasts(query).Result;

                cache.Received(0).UpdateCache(query.City, forecasts);
            }


            [Test]
            public void Should_return_weather_forecasts_form_cache_when_cache_contains_city()
            {
                cache.TryGetForecasts(DefaultCity, out var forecasts).Returns(true);

                var weatherForecastResult = watherController.WeatherForecasts(query).Result;

                weatherForecastResult.WeatherForecast.Should().BeEquivalentTo(forecasts);
                weatherForecastResult.Status.Should().Be(Status.Success);
            }
        }
    }
}