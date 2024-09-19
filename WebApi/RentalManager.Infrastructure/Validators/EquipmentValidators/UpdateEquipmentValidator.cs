using FluentValidation;
using RentalManager.Infrastructure.Models.Commands.EquipmentCommands;

namespace RentalManager.Infrastructure.Validators.EquipmentValidators;

public class UpdateEquipmentValidator : AbstractValidator<UpdateEquipmentCommand>
{
    public UpdateEquipmentValidator()
    {
        RuleFor(x => x)
            .SetValidator(new BaseEquipmentValidator());
    }
}