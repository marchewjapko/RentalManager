using FluentValidation;
using RentalManager.Infrastructure.Commands.UserCommands;

namespace RentalManager.Infrastructure.Validators.UserValidators;

public class CreateUserValidator : AbstractValidator<CreateUser>
{
    public CreateUserValidator()
    {
        RuleFor(x => x)
            .SetValidator(new UserBaseValidator());

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(100)
            .Must(x => x.Any(char.IsNumber))
            .WithMessage("Password must contain a number");

        RuleFor(x => x.UserName)
            .NotEmpty()
            .MaximumLength(100)
            .Matches(@"^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻĆ 0-9\/.\/+\-\/%]*$");
    }
}