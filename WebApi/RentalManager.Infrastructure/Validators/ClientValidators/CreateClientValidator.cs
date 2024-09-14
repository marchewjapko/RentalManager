using FluentValidation;
using RentalManager.Infrastructure.Models.Commands.ClientCommands;

namespace RentalManager.Infrastructure.Validators.ClientValidators;

public class CreateClientValidator : AbstractValidator<CreateClient>
{
    public CreateClientValidator()
    {
        RuleFor(x => x)
            .SetValidator(new ClientBaseValidator());
    }
}