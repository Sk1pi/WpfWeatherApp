using Models;

namespace Interfaces
{
    public interface IWeatherProvider
    {
        public string ProviderDomain { get; }

        public Task<WeatherForecast> Get3DayForecastAsync(float latitude, float longitude, ForecastMeasuresModel forecastMeasures);
    }
}