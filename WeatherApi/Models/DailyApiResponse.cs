namespace WeatherApi.Models
{
    public class DailyApiResponse
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public long DateEpoch { get; set; } // date_epoch
        public string Date { get; set; } // date
        public float Elevation { get; set; } // Додано
        public string? Timezone { get; set; } // Додано
        public string? Timezone_abbreviation { get; set; } // Додано
        public int Utc_offset_seconds { get; set; } // Додано

        public AstroInfo? AstroInfo { get; set; }
        public DailyData? Daily { get; set; }
        public HourlyData? Hourly { get; set; }
    }

    public class AstroInfo
    {
        public string? Time { get; set; }
        public string? sunrise { get; set; } // sunrise
        public string? maxtemp_c { get; set; }
        public string? mintemp_c { get; set; }
        public string? sunset { get; set; } // sunset
        public string? moonrise { get; set; } // moonrise
        public string? moonset { get; set; } // moonset
        public string? moon_phase { get; set; } // moon_phase
        public string? moon_illumination { get; set; } // moon_illumination
        public string? is_sun_up { get; set; } // is_sun_up
        public string? is_moon_up { get; set; } // is_moon_up
        public string? wind_dir { get; set; }
        public string? maxwind_mph { get; set; }
        public string? maxwind_kph { get; set; }
        public string? WeatherCode { get; set; }
        public string? precip_mm { get; set; }
        public string? will_it_rain { get; set; }
        public string? will_it_snow { get; set; }
    }

    public class DailyData
    {
        public List<DateTime>? Time { get; set; }
        public List<float>? Maxtemp_c { get; set; }
        public List<float>? Mintemp_c { get; set; }
        public List<float>? Precip_mm { get; set; }
        public List<float>? Will_it_rain { get; set; }
        public List<float>? Will_it_snow { get; set; }
        public List<DateTime>? Sunrise { get; set; }
        public List<DateTime>? Sunset { get; set; }
        public List<int>? Wind_dir { get; set; }
        public List<float>? Maxwind_mph { get; set; }
        public List<int>? WeatherCode { get; set; }
        public List<float>? Maxwind_kph { get; set; }
        public List<float>? Gust_mph { get; set; }
    }

    public class HourlyData
    {
        public List<DateTime>? Time { get; set; }
        public List<float>? Feelslike_c { get; set; }
        public List<float>? Temp_c { get; set; }
        public List<float>? Avgtemp_c { get; set; }
        public List<float>? Avghumidity { get; set; } // Added
        public List<float>? Maxwind_kph { get; set; }
        public List<float>? Pressure_mb { get; set; } // Added
        public List<int>? Wind_dir { get; set; }
        public List<int>? WeatherCode { get; set; }
    }
}
