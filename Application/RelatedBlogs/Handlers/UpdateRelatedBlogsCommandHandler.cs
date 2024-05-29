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
    public class UpdateRelatedBlogsCommandHandler : IRequestHandler<UpdateRelatedBlogsCommand, Blog>
    {
        private readonly IBlogRepository _blogRepository;

        public UpdateRelatedBlogsCommandHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<Blog> Handle(UpdateRelatedBlogsCommand request, CancellationToken cancellationToken)
        {
            var blog = await _blogRepository.GetByIdAsync(request.BlogId);
            if (blog == null)
            {
                throw new KeyNotFoundException($"Blog with ID {request.BlogId} not found.");
            }

            blog.RelatedBlogs = request.RelatedBlogIds;
            var result = await _blogRepository.UpdateAsync(blog);
            return result;
        }
    }
}
