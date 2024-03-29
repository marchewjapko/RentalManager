﻿using FluentValidation;
using RentalManager.Infrastructure.Commands.AgreementCommands;

namespace RentalManager.Infrastructure.Validators.AgreementValidators;

public class UpdateAgreementValidator : AbstractValidator<UpdateAgreement>
{
    public UpdateAgreementValidator()
    {
        RuleFor(x => x)
            .SetValidator(new AgreementBaseValidator());
    }
}