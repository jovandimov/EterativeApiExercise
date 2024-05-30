using BlogApi.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace BlogApi.Settings
{
    public class BlogContext
    {
        private readonly IMongoDatabase _database;

        public BlogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDb"));
            _database = client.GetDatabase("BlogDb");
        }

        public IMongoCollection<Blog> Blogs => _database.GetCollection<Blog>("Blogs");
    }
}