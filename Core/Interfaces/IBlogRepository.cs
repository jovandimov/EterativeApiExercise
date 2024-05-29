using BlogApi.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogApi.Core.Interfaces
{
    public interface IBlogRepository
    {
        Task<Blog> GetByIdAsync(string id);
        Task<IEnumerable<Blog>> GetAllAsync();
        Task AddAsync(Blog blog);
        Task<Blog> UpdateAsync(Blog blog);
        Task DeleteAsync(string id);
    }
}
