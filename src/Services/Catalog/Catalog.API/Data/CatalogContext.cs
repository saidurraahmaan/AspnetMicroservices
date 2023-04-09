using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        private readonly string _config;
        public CatalogContext(IConfiguration configuration)
        {
            _config = configuration.GetValue<string>("DatabaseSettings");
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionSettings"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
