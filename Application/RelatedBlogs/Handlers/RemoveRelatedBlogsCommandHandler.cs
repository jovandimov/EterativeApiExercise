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
    public class RemoveRelatedBlogsCommandHandler : IRequestHandler<RemoveRelatedBlogsCommand, Blog>
    {
        private readonly IBlogRepository _blogRepository;

        public RemoveRelatedBlogsCommandHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<Blog> Handle(RemoveRelatedBlogsCommand request, CancellationToken cancellationToken)
        {
            var blog = await _blogRepository.GetByIdAsync(request.BlogId);
            if (blog == null)
            {
                throw new KeyNotFoundException($"Blog with ID {request.BlogId} not found.");
            }

            blog.RelatedBlogs = blog.RelatedBlogs.Except(request.RelatedBlogIds).ToList();
            var result = await _blogRepository.UpdateAsync(blog);
            return result;
        }
    }
}
