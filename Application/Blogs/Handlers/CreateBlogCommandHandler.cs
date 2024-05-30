using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlogApi.Application.Commands;
using BlogApi.Core.Entities;
using BlogApi.Core.Interfaces;
using MediatR;

namespace BlogApi.Application.Handlers
{
    public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, string>
    {
        private readonly IBlogRepository _blogRepository;

        public CreateBlogCommandHandler(IBlogRepository blogRepository)
        {
            this._blogRepository = blogRepository;
        }

        public async Task<string> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            var blog = new Blog
            {
                Title = request.Title,
                Text = request.Text,
                CreatedOn = DateTime.UtcNow,
                RelatedBlogs = request.ReletadBlogs.ToList()
            };

            await _blogRepository.AddAsync(blog);
            return blog.Id;
        }
    }
}