using MediatR;
using BlogApi.Core.Entities;

namespace BlogApi.Application.Queries
{
    public class GetBlogByIdQuery : IRequest<Blog>
    {
        public string Id { get; set; } = string.Empty;
    }
}