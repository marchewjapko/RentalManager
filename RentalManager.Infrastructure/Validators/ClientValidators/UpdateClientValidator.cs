using FluentValidation;
using RentalManager.Infrastructure.Commands.ClientCommands;

namespace RentalManager.Infrastructure.Validators;

public class UpdateClientValidator : AbstractValidator<CreateClient>
{
    public UpdateClientValidator()
    {
        RuleFor(x => x)
            .SetValidator(new ClientBaseValidator());
    }
}