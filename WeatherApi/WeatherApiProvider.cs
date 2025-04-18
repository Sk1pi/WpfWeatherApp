using WeatherApi.Models;
using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using Interfaces;
using Models;
using System.Text.Json;
using System.Net.Http;


namespace WeatherApi
{
    public class WeatherApiProvider: IWeatherProvider
    {
        private readonly HttpClient _httpClient;

        public string ProviderDomain { get; } = "weatherapi.com"; 

        public WeatherApiProvider()
        {
            _httpClient = new() { BaseAddress = new Uri("https://api.weatherapi.com/v1/forecast/") };
        }

        public async Task<WeatherForecast> Get3DayForecastAsync(float latitude, float longitude, ForecastMeasuresModel measureConfig)
        {
            StringBuilder url = new();
            url.Append("?latitude=" + latitude.ToString(CultureInfo.InvariantCulture));
            url.Append("&longitude=" + longitude.ToString(CultureInfo.InvariantCulture));
            url.Append("&temperature_unit=" + measureConfig.TemperatureUnit.ToString().ToLower());
            url.Append("&windspeed_unit=" + measureConfig.WindSpeedUnit.ToString().ToLower());
            url.Append("&precipitation_unit=" + measureConfig.PrecipitationUnit.ToString().ToLower());
            url.Append("&latitude=" + latitude.ToString(CultureInfo.InvariantCulture));
            url.Append("&timezone=auto");
            url.Append("&past_days=2");
            url.Append("&daily=maxtemp_c,mintemp_c,precip_mm,will_it_rain,will_it_snow,sunrise,sunset,wind_dir,maxwind_mph,weatherCode,maxwind_kph,gust_mph");
            url.Append("&hourly=feelslike_c,temp_c,avgtemp_c,avghumidity,maxwind_kph,pressure_mb,wind_dir,weatherCode");

            DailyApiResponse response;
            try
            {
                response = await _httpClient.GetFromJsonAsync<DailyApiResponse>(url.ToString());
            }
            catch
            {
                return null!;
            }

            return MapToWeatherForecast(response!, measureConfig, latitude, longitude);
        }

        private WeatherForecast MapToWeatherForecast(DailyApiResponse apiResponse, ForecastMeasuresModel measureConfig, float latitude, float longitude)
        {
            // Мапінг даних із DailyApiResponse в WeatherForecast
            WeatherForecast forecast = new();

            forecast.Location = new()
            {
                Name = apiResponse.Timezone ?? "Unknown",
                Latitude = apiResponse.Latitude,
                Longitude = apiResponse.Longitude,
                Elevation = apiResponse.Elevation,
                Timezone = apiResponse.Timezone,
                TimezoneAbbreviation = apiResponse.Timezone_abbreviation,
            };
            forecast.Measures = new ForecastMeasuresModel()
            {
                TemperatureUnit = measureConfig.TemperatureUnit,
                PrecipitationUnit = measureConfig.PrecipitationUnit,
                WindSpeedUnit = measureConfig.WindSpeedUnit,
                PressureUnit = measureConfig.PressureUnit
            };
            //StartDate = DateTime.UtcNow,
            //EndDate = DateTime.UtcNow.AddDays(3),
            int hoursCounter = 0;
            for (int i = 0; i < apiResponse?.Daily?.Time?.Count; i++)
            {
                DayForecastModel day = new()
                {
                    Date = apiResponse.Daily.Time[i],
                    Weather = (WeatherCodes)apiResponse.Daily.WeatherCode[i],
                    MaxTemperature = apiResponse.Daily.Maxtemp_c[i],
                    MinTemperature = apiResponse.Daily.Mintemp_c[i],
                    SumPrecipitation = apiResponse.Daily.Precip_mm[i],
                    WindSpeed = apiResponse.Daily.Maxwind_kph[i],
                    WindDirection = apiResponse.Daily.Wind_dir[i],
                    Sunrise = apiResponse.Daily.Sunrise[i],
                    Sunset = apiResponse.Daily.Sunset[i],
                    DayTime = apiResponse.Daily.Sunset[i] - apiResponse.Daily.Sunrise[i],
                };
                float pressure = 0;
                for (int j = hoursCounter; j < hoursCounter + 24; j++)
                {
                    HourlyForecastModel hour = new()
                    {
                        Time = apiResponse.Hourly.Time[j],
                        Temperature = apiResponse.Hourly.Temp_c[j],
                        ApparentTemperature = apiResponse.Hourly.Avgtemp_c[j],
                        RelativeHumidity  = apiResponse.Hourly.Avghumidity[j],
                        WindSpeed  = apiResponse.Hourly.Maxwind_kph[j],
                        WindDirection  = apiResponse.Hourly.Wind_dir[j],
                        Weather = (WeatherCodes)apiResponse.Hourly.WeatherCode[j],
                    };
                    hour.SurfacePressure = apiResponse.Hourly.Pressure_mb[j];
                    if(measureConfig.PressureUnit == PressureMeasure.MmHg)
                        hour.SurfacePressure *= 0.7501f;
                    pressure += hour.SurfacePressure;
                    day.HourlyForecasts.Add(hour);
                }

                day.MoonState = GetMoonPhaze(day.Date);
                day.Pressure = pressure / 24;
                hoursCounter += 24;
                forecast.DayForecasts.Add(day);
            }

            forecast.StartDate = apiResponse.Daily.Time.First();
            forecast.EndDate = apiResponse.Daily.Time.Last();

            return forecast;
        }

        private MoonPhazes GetMoonPhaze(DateTime date)
        {
            double year = date.Year;
            double month = date.Month;
            if (month < 3)
            {
                month += 12;
                year -= 1;
            }

            double julianDays = Math.Truncate(365.25 * (year + 4716)) + Math.Truncate(30.6001 * (month + 1)) + date.Day - 1524.5 - 13;

            double ip = (julianDays - 2451550.1) / 29.530588853;
            ip -= Math.Floor(ip);

            if (ip < 0) ip += 1;
            double age = ip * 29.53;

            if (age < 1.84566) return MoonPhazes.New;
            if (age < 12.91963) return MoonPhazes.FirstQuarter;
            if (age < 16.61096) return MoonPhazes.Full;
            if (age < 27.68493) return MoonPhazes.LastQuarter;
            return MoonPhazes.New;
        }
    }
}
