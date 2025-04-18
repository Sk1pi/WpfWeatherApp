using GeoCoder.Models;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace MyWeatherAppp.Services
{
    internal static class SettingsService
    {
        private static readonly string _pathToFile = Path.Combine(Environment.CurrentDirectory, "appsettings.json");

        public static Configuration Configuration { get; } = CreateDefaultConfiguration();

        public static void SaveChanges()
        {
            File.WriteAllText(_pathToFile, JsonConvert.SerializeObject(Configuration));
        }

        private static Configuration CreateDefaultConfiguration()
        {
            if (!File.Exists(_pathToFile))
                File.Create(_pathToFile).Close();

            string jsonContent = File.ReadAllText(_pathToFile);
            Configuration? defaultConfig = null;

            if (!string.IsNullOrWhiteSpace(jsonContent))
            {
                defaultConfig = JsonConvert.DeserializeObject<Configuration>(jsonContent);
            }

            if (defaultConfig == null)
            {
                defaultConfig /*??*/= new Configuration()
                {
                ApiKey = "f75e9056ea704db2a67195856250701 ",
                Culture = Cultures.EN,
                Lenght = LenghtMeasure.Mm,
                Temperature = TemperatureMeasure.Celsius,
                Pressure = PressureMeasure.MmHg,
                Wind = WindSpeed.Ms,
                SelectedLocation = new GeoLocationsModels
                {
                    Latitude = 54.629566f,
                    Longitude = 39.741917f,
                    Name = "Київ",
                    Description = "Україна"
                }
                };

            }

            //if (defaultConfig.SelectedLocation == null)
            //{
            //    defaultConfig.SelectedLocation = new GeoLocationsModels
            //    {
            //        Latitude = 37.208171f,
            //        Longitude = -93.292274f,
            //        Name = "Springfield",
            //        Description = "Springfield, United States"
            //    };
            //}

            //if (!defaultConfig.FavouritLocations.Contains(defaultConfig.SelectedLocation))
            //{
            //    defaultConfig.FavouritLocations.Add(defaultConfig.SelectedLocation); // перевірити, якщо буде помилка 
            //}
            //ServiceManager.LocalizationService.SetCulture(defaultConfig.Culture);
            //return defaultConfig;

            if (string.IsNullOrEmpty(defaultConfig.ApiKey))
            {
                defaultConfig.ApiKey = "f75e9056ea704db2a67195856250701"; // Перевіряємо, чи є API-ключ
            }

            ServiceManager.LocalizationService.SetCulture(defaultConfig.Culture);
            return defaultConfig;
        }
    }

    public class Configuration
    {
        public string ApiKey { get; set; } = "f75e9056ea704db2a67195856250701 ";
        public GeoLocationsModels SelectedLocation { get; set; }
        public ObservableCollection<GeoLocationsModels> FavouritLocations { get; set; } = new();
        public Cultures Culture { get; set; }
        public bool IsSavingTrafficModeEnabled { get; set; }
        public TemperatureMeasure Temperature { get; set; }
        public PressureMeasure Pressure { get; set; }
        public LenghtMeasure Lenght { get; set; }
        public WindSpeed Wind { get; set; }
    }
}
