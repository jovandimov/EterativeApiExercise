using BlogApi.Core.Entities;
using MediatR;

namespace BlogApi.Application.Commands
{
    public class DeleteBlogCommand : IRequest<Blog>
    {
        public string Id { get; set; } = string.Empty;
    }
}