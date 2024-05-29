using MediatR;
using BlogApi.Core.Entities;

namespace BlogApi.Application.Queries
{
    public class GetAllBlogsQuery : IRequest<IEnumerable<Blog>> { }
}