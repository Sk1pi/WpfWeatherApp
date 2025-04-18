using MongoDB.Bson;
using MongoDB.Driver;
using GeoCoder.Models;
using GeoCoder.Data; // Баг з папками
using GeoCoder.Cache;


namespace GeoCoder.Data
{
    internal static class DataRepository
    {
        public static List<GeoLocationsModels> Find(string place)
        {
            var program = new MongoDbDataBase();
            var collection = program.GetCollection<GeoLocationsModels>();

            var filter = Builders<GeoLocationsModels>.Filter.Regex("name", new BsonRegularExpression(place, "i"));

            return collection.Find(filter).ToList();
        }

        public static void SaveArrange(List<GeoLocationsModels> locations)
        {
            var program = new MongoDbDataBase();
            var collection = program.GetCollection<GeoLocationsModels>();

            foreach (var item in locations)
            {
                var filter = Builders<GeoLocationsModels>.Filter.And(
                    Builders<GeoLocationsModels>.Filter.Eq("latitude", item.Latitude),
                    Builders<GeoLocationsModels>.Filter.Eq("longitude", item.Longitude)
                );

                if (collection.Find(filter).FirstOrDefault() == null)
                {
                    collection.InsertOne(item);
                }
            }
        }
    }
}
