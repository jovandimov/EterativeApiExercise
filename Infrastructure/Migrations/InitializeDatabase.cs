using MongoDB.Driver;
using BlogApi.Core.Entities;
using BlogApi.Settings;

namespace BlogApi.Infrastructure.Migrations
{
    public class InitializeDatabase
    {
        private readonly IMongoDatabase _database;

        public InitializeDatabase(MongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);
        }

        public async Task Run()
        {
            await CreateIndexes();

            await SeedData();
        }

        private async Task CreateIndexes()
        {
            var blogsCollection = _database.GetCollection<Blog>("Blogs");

            var indexKeysDefinition = Builders<Blog>.IndexKeys.Descending(blog => blog.CreatedOn);
            var indexModel = new CreateIndexModel<Blog>(indexKeysDefinition);
            await blogsCollection.Indexes.CreateOneAsync(indexModel);
        }

        private async Task SeedData()
        {
            var blogsCollection = _database.GetCollection<Blog>("Blogs");

            var existingBlogs = await blogsCollection.Find(_ => true).AnyAsync();
            if (!existingBlogs)
            {
                var initialBlogs = new List<Blog>
                {
                    new Blog
                    {
                        Title = "First Blog Post",
                        Text = "This is the text of the first blog post.",
                        CreatedOn = DateTime.UtcNow,
                        ModifiedOn = null,
                        DeletedOn = null,
                        RelatedBlogs = new List<string>()
                    },
                    new Blog
                    {
                        Title = "Second Blog Post",
                        Text = "This is the text of the second blog post.",
                        CreatedOn = DateTime.UtcNow,
                        ModifiedOn = null,
                        DeletedOn = null,
                        RelatedBlogs = new List<string>()
                    }
                };

                await blogsCollection.InsertManyAsync(initialBlogs);
            }
        }
    }
}
