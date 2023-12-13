﻿using FluentValidation;
using RentalManager.Infrastructure.Commands.EquipmentCommands;

namespace RentalManager.Infrastructure.Validators;

public class CreateEquipmentValidator : AbstractValidator<CreateEquipment>
{
    public CreateEquipmentValidator()
    {
        RuleFor(x => x)
            .SetValidator(new EquipmentBaseValidator());
    }
}