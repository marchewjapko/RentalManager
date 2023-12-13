using FluentValidation;
using RentalManager.Infrastructure.Commands.ClientCommands;

namespace RentalManager.Infrastructure.Validators;

public class CreateClientValidator : AbstractValidator<CreateClient>
{
    public CreateClientValidator()
    {
        RuleFor(x => x)
            .SetValidator(new ClientBaseValidator());
    }
}