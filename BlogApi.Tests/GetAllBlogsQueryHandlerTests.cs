using BlogApi.Application.Handlers;
using BlogApi.Application.Queries;
using BlogApi.Core.Entities;
using BlogApi.Core.Interfaces;
using Moq;
using Xunit;

public class GetAllBlogsQueryHandlerTests
{
    private readonly Mock<IBlogRepository> _blogRepositoryMock;
    private readonly GetAllBlogsQueryHandler _handler;

    public GetAllBlogsQueryHandlerTests()
    {
        _blogRepositoryMock = new Mock<IBlogRepository>();
        _handler = new GetAllBlogsQueryHandler(_blogRepositoryMock.Object);
    }


    [Fact]
    public async Task HandleShouldReturnAllBlogs()
    {
        var blogs = new List<Blog> { new Blog { Id = "123" } };
        _blogRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(blogs);
        var query = new GetAllBlogsQuery();

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.Equal(blogs, result);
    }
}