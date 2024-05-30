using BlogApi.Core.Entities;
using BlogApi.Core.Interfaces;
using BlogApi.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BlogApi.Infrastructure.Data
{
    public class BlogRepository : IBlogRepository
    {

        private readonly IMongoCollection<Blog> _blogs;

        public BlogRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _blogs = database.GetCollection<Blog>("Blogs");
        }

        public async Task AddAsync(Blog blog)
        {
            await _blogs.InsertOneAsync(blog);
        }

        public async Task DeleteAsync(string id)
        {
            await _blogs.DeleteOneAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await _blogs.Find(blog => blog.DeletedOn == null).ToListAsync();
        }

        public async Task<Blog> GetByIdAsync(string id)
        {
            return await _blogs.Find(blog => blog.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Blog> UpdateAsync(Blog blog)
        {
            var result = await _blogs.ReplaceOneAsync(b => b.Id == blog.Id, blog);
            if (result.IsAcknowledged && result.ModifiedCount > 0)
            {
                return await GetByIdAsync(blog.Id);
            }
            else
            {
                return null;
            }
        }
    }
}