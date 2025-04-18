using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoCoder.Cache
{
    public class MongoDbDataBase
    {
        private readonly IMongoDatabase _database;

        public MongoDbDataBase()
        {
            var connectionData = "mongodb://localhost:27017/";
            var databaseName = "MyDataBase";
            var client = new MongoClient(connectionData);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            var collectionName = "GeoLocationsModels";
            return _database.GetCollection<T>(collectionName);
        }

    }
}
