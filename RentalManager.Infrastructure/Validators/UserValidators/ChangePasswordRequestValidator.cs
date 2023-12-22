using FluentValidation;
using RentalManager.Global.Requests;

namespace RentalManager.Infrastructure.Validators.UserValidators;

public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .MaximumLength(100)
            .Matches(@"^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻĆ 0-9\/.\/+\-\/%]*$");

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(100)
            .Must(x => x.Any(char.IsNumber))
            .WithMessage("Password must contain a number")
            .Must(x => x.Any(char.IsUpper))
            .WithMessage("Password must contain a uppercase letter")
            .Must(x => x.Any(char.IsLower))
            .WithMessage("Password must contain a lowercase letter")
            .Must(x => x.Any(c => char.IsSymbol(c) || char.IsPunctuation(c)))
            .WithMessage("Password must contain a symbol");
    }
}