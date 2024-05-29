using BlogApi.Application.Commands;
using BlogApi.Core.Entities;
using BlogApi.Core.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace BlogApi.Application.Handlers
{
    public class AddRelatedBlogsCommandHandler : IRequestHandler<AddRelatedBlogsCommand,Blog>
    {
        private readonly IBlogRepository _blogRepository;

        public AddRelatedBlogsCommandHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<Blog> Handle(AddRelatedBlogsCommand request, CancellationToken cancellationToken)
        {
            var blog = await _blogRepository.GetByIdAsync(request.BlogId);
            if (blog == null)
            {
                throw new KeyNotFoundException($"Blog with ID {request.BlogId} not found.");
            }

            foreach (var relatedBlogId in request.RelatedBlogIds)
            {
                if (!blog.RelatedBlogs.Contains(relatedBlogId))
                {
                    blog.RelatedBlogs.Add(relatedBlogId);
                }
            }

            var result = await _blogRepository.UpdateAsync(blog);
            return result;
        }
    }
}