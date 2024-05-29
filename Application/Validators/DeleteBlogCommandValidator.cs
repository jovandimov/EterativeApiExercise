using BlogApi.Application.Commands;
using FluentValidation;

public class DeleteBlogCommandValidator : AbstractValidator<DeleteBlogCommand>
{
    public DeleteBlogCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Blog id should not be empty");
    }
}