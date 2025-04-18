using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GeoCoder.Models
{
    public class GeoLocationsModels
    {
        [BsonId] // Унікальний ідентифікатор для MongoDB
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("latitude")] // Задає назву поля у колекції
        public float Latitude { get; set; }

        [BsonElement("longitude")]
        public float Longitude { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string? Description { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is GeoLocationsModels geoLocation)
            {
                return this.Latitude == geoLocation.Latitude && this.Longitude == geoLocation.Longitude;
            }
            return base.Equals(obj);
        }
    }
}
