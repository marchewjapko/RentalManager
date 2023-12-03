using FluentValidation;
using RentalManager.Infrastructure.Commands.ClientCommands;

namespace RentalManager.Infrastructure.Services.Validators;

public class ClientValidator : AbstractValidator<ClientBaseCommand>
{
    public ClientValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100)
            .Matches(@"^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻ]*$")
            .WithMessage("'Name' should only contain letters");

        RuleFor(x => x.Surname)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100)
            .Matches(@"^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻ]*$")
            .WithMessage("'Surname' should only contain letters");

        RuleFor(x => x.PhoneNumber)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100)
            .Matches(
                @"^((\+[0-9]{1,4})?[-. ]?([0-9]{3})[-. ]?([0-9]{3})[-. ]?([0-9]{3}))$|^(\(?[0-9]{2}\)?[-. ]?[0-9]{2}[-. ]?[0-9]{3}[-. ]?[0-9]{2})$");

        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(100)
            .Matches(@"^[a-zA-Z]*\@[a-zA-Z]{3,}\.[a-zA-Z]{2,5}$");

        RuleFor(x => x.City)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100)
            .Matches(@"^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻ ]*$");

        RuleFor(x => x.Street)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100)
            .Matches(@"^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻ 0-9\/.]*$")
            .WithMessage("'Street' should only contain letter, numbers, digits, slashes and dots");
    }
}