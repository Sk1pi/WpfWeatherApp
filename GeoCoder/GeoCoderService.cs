using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using GeoCoder.Models;
using GeoCoder.Data;

namespace GeoCoder
{
   public static class GeoCoderService
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly string _apikey = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Apikey.ini")).Trim();

        public async static Task<List<GeoLocationsModels>> GetPositionAsync(string place)
        {
            // Перевіряємо кешовані дані
            var cachedLocations = Data.DataRepository.Find(place);
            if (cachedLocations.Count > 0)
                return cachedLocations;

            // Формування URL для запиту
            StringBuilder url = new();
            url.Append("https://api.opencagedata.com/geocode/v1/json?");
            url.Append("q=" + Uri.EscapeDataString(place)); // Коректний параметр запиту
            url.Append("&limit=20"); // Максимальна кількість результатів
            url.Append("&key=" + _apikey);

            ApiResponse response;
            try
            {
                // Надсилаємо запит до API
                response = await _httpClient.GetFromJsonAsync<ApiResponse>(new Uri(url.ToString()));
                if (response == null)
                    throw new Exception("Порожня відповідь від API.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при запиті до API: {ex.Message}");
                return null!;
            }

            // Конвертуємо дані у внутрішню модель
            var model = ToUserModel(response);

            // Зберігаємо результати в базу даних
            Data.DataRepository.SaveArrange(model);
            return model;
        }

        private static List<GeoLocationsModels> ToUserModel(ApiResponse response)
        {
            List<GeoLocationsModels> locations = new();

            foreach (var item in response.Results)
            {
                GeoLocationsModels location = new GeoLocationsModels
                {
                    Name = item.Components.Road ?? item.Components.City ?? "Unknown",
                    Description = $"{item.Components.City}, {item.Components.Country}",
                    Latitude = (float)item.Geometry.Lat,
                    Longitude = (float)item.Geometry.Lng
                };

                locations.Add(location);
            }

            return locations;
        }
    }
}
