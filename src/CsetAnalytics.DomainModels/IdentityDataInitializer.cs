using System;
using System.Linq;
using System.Threading.Tasks;
using CsetAnalytics.DomainModels.Models;
using CsetAnalytics.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace CsetAnalytics.DomainModels
{
    public class DatabaseInitializer
    {

        public static async Task SeedCollections(IMongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            if (client != null)
            {
                var database = client.GetDatabase(settings.DatabaseName);
                await CreateCollection(database, "assessments");
                await CreateCollection(database, "analytics_questionanswer");
                await CreateCollection(database, "sector_industries");
            }
            
        }
        
        private static async Task CreateCollection(IMongoDatabase database, string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);
            var collectionCursor = await database.ListCollectionsAsync(new ListCollectionsOptions { Filter = filter });
            if (!collectionCursor.Any())
            {
                database.CreateCollection(collectionName);
            }
        }
    }
}
