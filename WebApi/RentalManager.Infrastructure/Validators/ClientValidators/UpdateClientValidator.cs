using FluentValidation;
using RentalManager.Infrastructure.Models.Commands.ClientCommands;

namespace RentalManager.Infrastructure.Validators.ClientValidators;

public class UpdateClientValidator : AbstractValidator<UpdateClient>
{
    public UpdateClientValidator()
    {
        RuleFor(x => x)
            .SetValidator(new ClientBaseValidator());
    }
}