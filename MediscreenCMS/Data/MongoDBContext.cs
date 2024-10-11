using MedisrceenCMS.Models.Models; // Adjust this namespace if your models are in a different namespace
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace MediscreenCMS.Data
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;
        private readonly IConfiguration _configuration; // Step 1: Add this line

        // Constructor to initialize the MongoDB connection using the configuration settings
        public MongoDBContext(IConfiguration configuration)
        {
            _configuration = configuration; // Step 2: Store the configuration in a class-level field

            var client = new MongoClient(_configuration.GetConnectionString("MongoDBConnection"));

            if (client != null)
            {
                _database = client.GetDatabase(_configuration["MongoDBSettings:DatabaseName"]);
            }
        }

        // This property provides access to the Notes collection in MongoDB
        public IMongoCollection<Note> Notes
        {
            get
            {
                return _database.GetCollection<Note>(_configuration["MongoDBSettings:CollectionName"]); // Step 3: Use _configuration instead of configuration
            }
        }
    }
}


