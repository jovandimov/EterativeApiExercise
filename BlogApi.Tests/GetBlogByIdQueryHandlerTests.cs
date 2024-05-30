using BlogApi.Application.Handlers;
using BlogApi.Application.Queries;
using BlogApi.Core.Entities;
using BlogApi.Core.Interfaces;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class GetBlogByIdQueryHandlerTests
{
    private readonly Mock<IBlogRepository> _blogRepositoryMock;
    private readonly GetBlogByIdQueryHandler _handler;

    public GetBlogByIdQueryHandlerTests()
    {
        _blogRepositoryMock = new Mock<IBlogRepository>();
        _handler = new GetBlogByIdQueryHandler(_blogRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_Blog_When_Found()
    {
        // Arrange
        var blog = new Blog { Id = "abc123f1" };
        _blogRepositoryMock.Setup(repo => repo.GetByIdAsync("abc123f1")).ReturnsAsync(blog);
        var query = new GetBlogByIdQuery { Id = "abc123f1" };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(blog, result);
    }

    [Fact]
    public async Task Handle_Should_Return_Null_When_Not_Found()
    {
        _blogRepositoryMock.Setup(repo => repo.GetByIdAsync("abc123f1")).ReturnsAsync((Blog)null);
        var query = new GetBlogByIdQuery { Id = "abc123f1" };

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.Null(result);
    }
}
