using System;
using System.Threading.Tasks;
using GeoCoder;

class Program
{
    static async Task Main(string[] args)
    {
        var results = await GeoCoderService.GetPositionAsync("Springfield");

        if (results != null)
        {
            foreach (var location in results)
            {
                Console.WriteLine($"Name: {location.Name}");
                Console.WriteLine($"Latitude: {location.Latitude}");
                Console.WriteLine($"Longitude: {location.Longitude}");
                Console.WriteLine($"Description: {location.Description}");
                Console.WriteLine(new string('-', 20));
            }
        }
        else
        {
            Console.WriteLine("No results found or an error occurred.");
        }
    }
}
