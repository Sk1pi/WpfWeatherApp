using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Interfaces;
using WeatherApi;

namespace WeatherProvider
{
    public static class WeatherService 
    {
        private static IWeatherProvider _weatherProvider = null!;
        //private static string _apiKey = null!;

        //public static void Initialize(string apiKey)
        //{
        //    _apiKey = apiKey;
        //    WeatherApiProvider();
        //}

        public static void WeatherApiProvider()
        {
            if (_weatherProvider == null || !_weatherProvider.GetType().IsAssignableFrom(typeof(WeatherApiProvider)))
                _weatherProvider = new WeatherApiProvider();
        }

        public static string ProviderDomain => _weatherProvider.ProviderDomain;

        public static ForecastMeasuresModel MeasureConfiguration { get; } = new ForecastMeasuresModel()
        {
            TemperatureUnit = TemperatureMeasure.Celsius,
            PrecipitationUnit = LenghtMeasure.Mm,
            WindSpeedUnit = WindSpeed.Kmh,
            PressureUnit = PressureMeasure.HPa,
            RainUnit = LenghtMeasure.Mm,
            ShowersUnit = LenghtMeasure.Mm,
            SnowfallUnit = LenghtMeasure.Cm,
        };

        public static async Task<WeatherForecast>? Get3DayForecastAsync(float latitude, float longitude, string name = null)
        {
            //check cache
            try
            {
                var model = await _weatherProvider.Get3DayForecastAsync(latitude, longitude, MeasureConfiguration);
                model.Location.Name = name;
                return model;
            }
            catch (Exception)
            {
                return null;
            }
            //save in cache
        }
    }
}
