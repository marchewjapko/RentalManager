using FluentValidation;
using RentalManager.Infrastructure.Models.Commands.EquipmentCommands;

namespace RentalManager.Infrastructure.Validators.EquipmentValidators;

public class CreateEquipmentValidator : AbstractValidator<CreateEquipment>
{
    public CreateEquipmentValidator()
    {
        RuleFor(x => x)
            .SetValidator(new EquipmentBaseValidator());
    }
}