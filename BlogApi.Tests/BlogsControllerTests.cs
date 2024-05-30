using BlogApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Threading.Tasks;
using BlogApi.Application.Commands;
using BlogApi.Core.Entities;
using BlogApi.Application.Queries;


public class BlogsControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly BlogsController _controller;

    public BlogsControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new BlogsController(_mediatorMock.Object);
    }

    [Fact]
    public async Task CreateBlog_Should_Return_CreatedAtActionResult()
    {
        // Arrange
        var command = new CreateBlogCommand { Title = "Title", Text = "Text", ReletadBlogs = new List<string> { "123" } };
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateBlogCommand>(), default)).ReturnsAsync("1");

        // Act
        var result = await _controller.CreatedBlog(command);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal("GetBlogById", createdAtActionResult.ActionName);
    }

    [Fact]
    public async Task GetBlogById_Should_Return_OkObjectResult_With_Blog()
    {
        var blog = new Blog { Id = "1", Title = "Test Blog" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetBlogByIdQuery>(), default)).ReturnsAsync(blog);

        var result = await _controller.GetBlogById(blog.Id);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedBlog = Assert.IsType<Blog>(okResult.Value);
        Assert.Equal(blog.Id, returnedBlog.Id);
    }

    [Fact]
    public async Task GetAllBlogs_Should_Return_OkObjectResult_With_Blogs()
    {
        var blogs = new List<Blog> { new Blog { Id = "1", Title = "Test Blog" } };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllBlogsQuery>(), default)).ReturnsAsync(blogs);

        var result = await _controller.GetAllBlogs();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedBlogs = Assert.IsAssignableFrom<IEnumerable<Blog>>(okResult.Value);
        Assert.Single(returnedBlogs);
    }
}