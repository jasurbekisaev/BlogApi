using BlogApi.DtoModels.UserDtoModel;
using FluentValidation;

namespace BlogApi.Validators;

public class UserValidator : AbstractValidator<CreateUserDto>
{

    public UserValidator()
    {
        RuleFor(u => u.Username)
            .NotNull()
            .Must(u => u.EndsWith("@gmail.com"))
            .WithMessage("Email address must end with @gmail.com");

        RuleFor(u => u.Password)
            .NotNull()
            .Must(password => ValidatePassword(password))
            .WithMessage("Password must contain at least one capital letter, one digit, and one special character.");

        RuleFor(u => u.Name)
            .NotNull();
    }

    private bool ValidatePassword(string password)
    {
        if (string.IsNullOrEmpty(password))
            return false;

        bool hasCapitalLetter = false;
        bool hasDigit = false;
        bool hasSpecialCharacter = false;

        foreach (char c in password)
        {
            if (char.IsUpper(c))
                hasCapitalLetter = true;
            else if (char.IsDigit(c))
                hasDigit = true;
            else if (char.IsLetter(c) && c != '_' && c != '#')
                hasSpecialCharacter = true;

            if (hasCapitalLetter && hasDigit && hasSpecialCharacter)
                return true;
        }

        return false;
    }

}
