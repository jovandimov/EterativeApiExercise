using BlogApi.Core.Entities;
using MediatR;

namespace BlogApi.Application.Commands
{
    public class UpdateBlogCommand : IRequest<Blog>
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
    }
}