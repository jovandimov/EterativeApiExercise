using BlogApi.Core.Entities;
using BlogApi.Core.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlogApi.Application.Queries
{
    public class GetRelatedBlogsQueryHandler : IRequestHandler<GetRelatedBlogsQuery, List<Blog>>
    {
        private readonly IBlogRepository _blogRepository;

        public GetRelatedBlogsQueryHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<List<Blog>> Handle(GetRelatedBlogsQuery request, CancellationToken cancellationToken)
        {
            var blog = await _blogRepository.GetByIdAsync(request.BlogId);
            if (blog == null)
            {
                throw new KeyNotFoundException($"Blog with ID {request.BlogId} not found.");
            }

            var relatedBlogs = new List<Blog>();
            foreach (var relatedBlogId in blog.RelatedBlogs)
            {
                var relatedBlog = await _blogRepository.GetByIdAsync(relatedBlogId);
                if (relatedBlog != null)
                {
                    relatedBlogs.Add(relatedBlog);
                }
            }

            return relatedBlogs;
        }
    }
}
