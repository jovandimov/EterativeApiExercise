
using BlogApi.Application.Commands;
using FluentValidation;
using FluentValidation.AspNetCore;


public class UpdateBlogCommandValidator : AbstractValidator<UpdateBlogCommand>
{
    public UpdateBlogCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title must not be empty");
        RuleFor(x => x.Text).NotEmpty().WithMessage("Text should not be empty");
    }
}