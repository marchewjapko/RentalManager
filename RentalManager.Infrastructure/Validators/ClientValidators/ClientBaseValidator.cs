using FluentValidation;
using RentalManager.Infrastructure.Commands.ClientCommands;

namespace RentalManager.Infrastructure.Validators;

public class ClientBaseValidator : AbstractValidator<ClientBaseCommand>
{
    public ClientBaseValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100)
            .Matches("^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻĆ ']*$")
            .WithMessage("'Name' should only contain letters");

        RuleFor(x => x.Surname)
            .NotEmpty()
            .MaximumLength(100)
            .Matches("^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻĆ ']*$")
            .WithMessage("'Surname' should only contain letters");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .MaximumLength(100)
            .Matches(
                @"^((\+[0-9]{1,4})?[-. ]?([0-9]{3})[-. ]?([0-9]{3})[-. ]?([0-9]{3}))$|^(\(?[0-9]{2}\)?[-. ]?[0-9]{2}[-. ]?[0-9]{3}[-. ]?[0-9]{2})$");

        RuleFor(x => x.Email)
            .NotEmpty()
            .When(x => x.Email is not null)
            .MaximumLength(100)
            .Matches(@"^[a-zA-Z 0-9 \._]*\@[a-zA-Z 0-9 \._]{3,}\.[a-zA-Z]{2,5}$");

        RuleFor(x => x.IdCard)
            .NotEmpty()
            .MaximumLength(10)
            .Matches("^([a-zA-Z]){3}[ ]?[0-9]{6}$");

        RuleFor(x => x.City)
            .NotEmpty()
            .MaximumLength(100)
            .Matches(@"^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻĆ \-()]*$");

        RuleFor(x => x.Street)
            .NotEmpty()
            .MaximumLength(100)
            .Matches(@"^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻĆ 0-9\/.]*$")
            .WithMessage("'Street' should only contain letter, numbers, digits, slashes and dots");
    }
}