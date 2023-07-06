using BlogApi.Entities;
using FluentValidation;

namespace BlogApi.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(u => u.UserName).NotNull().MinimumLength(8).Must(u => u.EndsWith("@gmail.com")).WithMessage("Ohiri @gmail.com bilan tugashi kerak");
        RuleFor(u => u.Name).NotNull();
    }
}
