using BlogApi.Application.Commands;
using BlogApi.Application.Handlers;
using BlogApi.Core.Entities;
using BlogApi.Core.Interfaces;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class UpdateBlogCommandHandlerTests
{
    private readonly Mock<IBlogRepository> _blogRepositoryMock;
    private readonly UpdateBlogCommandHandler _handler;

    public UpdateBlogCommandHandlerTests()
    {
        _blogRepositoryMock = new Mock<IBlogRepository>();
        _handler = new UpdateBlogCommandHandler(_blogRepositoryMock.Object);
    }

    [Fact]
    public async Task HandleShouldUpdateBlog()
    {
        // Arrange
        var blog = new Blog { Id = "abc123", Title = "old", Text = "old" };
        var updatedBlog = new Blog { Id = "abc123", Title = "new", Text = "new" };

        _blogRepositoryMock.Setup(repo => repo.GetByIdAsync("abc123")).ReturnsAsync(blog);
        _blogRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Blog>())).ReturnsAsync(updatedBlog);

        var command = new UpdateBlogCommand { Id = "abc123", Title = "new", Text = "new" };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(updatedBlog, result);
        _blogRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<Blog>(b => b.Id == "abc123" && b.Title == "new" && b.Text == "new")), Times.Once);
    }

    [Fact]
    public async Task HandleShouldThrowExceptionWhenBlogNotFound()
    {
        _blogRepositoryMock.Setup(repo => repo.GetByIdAsync("123")).ReturnsAsync((Blog)null);

        var command = new UpdateBlogCommand { Id = "123", Title = "New Title", Text = "New Text" };

        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
    }
}
