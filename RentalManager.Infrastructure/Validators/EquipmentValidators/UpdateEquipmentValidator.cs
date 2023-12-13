using FluentValidation;
using RentalManager.Infrastructure.Commands.EquipmentCommands;

namespace RentalManager.Infrastructure.Validators;

public class UpdateEquipmentValidator : AbstractValidator<CreateEquipment>
{
    public UpdateEquipmentValidator()
    {
        RuleFor(x => x)
            .SetValidator(new EquipmentBaseValidator());
    }
}