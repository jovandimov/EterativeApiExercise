using BlogApi.Core.Entities;
using MediatR;
using System.Collections.Generic;

namespace BlogApi.Application.Queries
{
    public class GetRelatedBlogsQuery : IRequest<List<Blog>>
    {
        public string BlogId { get; set; } = string.Empty;
    }
}
