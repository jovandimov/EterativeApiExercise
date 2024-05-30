using BlogApi.Application.Commands;
using BlogApi.Application.Handlers;
using BlogApi.Core.Interfaces;
using Moq;
using Xunit;

public class DeleteBlogCommandHandlerTests
{
    private readonly Mock<IBlogRepository> _blogRepositoryMock;
    private readonly DeleteBlogCommandHandler _handler;

    public DeleteBlogCommandHandlerTests()
    {
        _blogRepositoryMock = new Mock<IBlogRepository>();
        _handler = new DeleteBlogCommandHandler(_blogRepositoryMock.Object);
    }
    

    [Fact]
    public async Task Handle_Should_call_DeleteAsync_Once()
    {
        
        // Arrange
        var command = new DeleteBlogCommand { Id = "123" };

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _blogRepositoryMock.Verify(repo => repo.DeleteAsync(command.Id), Times.Once);
    }
}