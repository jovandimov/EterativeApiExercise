using BlogApi.Application.Commands;
using BlogApi.Core.Entities;
using BlogApi.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlogApi.Application.Handlers
{
    public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand, Blog>
    {
        private readonly IBlogRepository _blogRepository;

        public UpdateBlogCommandHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<Blog> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            var blog = await _blogRepository.GetByIdAsync(request.Id);
            if (blog == null)
            {
                throw new Exception("Blog not found");
            }

            blog.Title = request.Title;
            blog.Text = request.Text;
            blog.ModifiedOn = DateTime.UtcNow;

            return await _blogRepository.UpdateAsync(blog);
            
        }
    }
}