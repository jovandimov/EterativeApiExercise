using System.Net.Http.Headers;
using BlogApi.Application.Commands;
using FluentValidation.TestHelper;
using Xunit;

public class CreateBlogCommandValidatorTests
{
    private readonly CreateBlogCommandValidator _validator;

    public CreateBlogCommandValidatorTests()
    {
        _validator = new CreateBlogCommandValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Title_Is_Empty()
    {
        var model = new CreateBlogCommand { Title = string.Empty, Text = "some text", ReletadBlogs = new List<string> { "123" } };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(blog => blog.Title);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Title_Is_Specified()
    {
        var model = new CreateBlogCommand { Title = "Title", Text = "Some text", ReletadBlogs = new List<string>() };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(blog => blog.Title);
    }
    [Fact]
    public void Should_Have_Error_When_Text_Is_Empty()
    {
        var model = new CreateBlogCommand { Title = "Title", Text = string.Empty, ReletadBlogs = new List<string> { "123" } };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(blog => blog.Text);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Text_Is_Specified()
    {
        var model = new CreateBlogCommand { Title = "some text", Text = "some text", ReletadBlogs = new List<string> { "123" } };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(blog => blog.Text);
    }
}