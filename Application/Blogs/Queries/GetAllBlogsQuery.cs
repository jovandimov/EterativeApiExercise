using MediatR;
using BlogApi.Core.Entities;
using System.Collections.Generic;

namespace BlogApi.Application.Queries
{
    public class GetAllBlogsQuery : IRequest<IEnumerable<Blog>> { }
}