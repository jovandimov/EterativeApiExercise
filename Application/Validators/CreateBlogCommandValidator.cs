
using BlogApi.Application.Commands;
using FluentValidation;

public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
{
    public CreateBlogCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Titl is required").MaximumLength(100).WithMessage("Title is too big");
        
        RuleFor(x => x.Text).NotEmpty().WithMessage("Text is required");
    }
}