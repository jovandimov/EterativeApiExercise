using MediatR;

namespace BlogApi.Application.Commands
{
    public class CreateBlogCommand : IRequest<string>
    {
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public List<string> ReletadBlogs { get; set; } = new List<string>();
    }
}