using BlogApi.Application.Commands;
using BlogApi.Core.Entities;
using BlogApi.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BlogApi.Application.Handlers
{
    public class DeleteBlogCommandHandler : IRequestHandler<DeleteBlogCommand, Blog>
    {
        private readonly IBlogRepository _blogRepository;

        public DeleteBlogCommandHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<Blog> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
        {
            await _blogRepository.DeleteAsync(request.Id);
            return null;
        }
    }
}