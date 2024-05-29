using BlogApi.Core.Entities;
using MediatR;
using System.Collections.Generic;

namespace BlogApi.Application.Commands
{
    public class RemoveRelatedBlogsCommand : IRequest<Blog>
    {
        public string BlogId { get; set; } = string.Empty;
        public List<string> RelatedBlogIds { get; set; } = new List<string>();
    }
}
