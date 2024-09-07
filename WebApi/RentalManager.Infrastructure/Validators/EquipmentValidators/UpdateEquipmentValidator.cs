using FluentValidation;
using RentalManager.Infrastructure.Commands.EquipmentCommands;

namespace RentalManager.Infrastructure.Validators.EquipmentValidators;

public class UpdateEquipmentValidator : AbstractValidator<UpdateEquipment>
{
    public UpdateEquipmentValidator()
    {
        RuleFor(x => x)
            .SetValidator(new EquipmentBaseValidator());
    }
}