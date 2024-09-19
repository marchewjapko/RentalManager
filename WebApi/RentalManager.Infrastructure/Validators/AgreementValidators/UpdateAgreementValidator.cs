using FluentValidation;
using RentalManager.Infrastructure.Models.Commands.AgreementCommands;

namespace RentalManager.Infrastructure.Validators.AgreementValidators;

public class UpdateAgreementValidator : AbstractValidator<UpdateAgreementCommand>
{
    public UpdateAgreementValidator()
    {
        RuleFor(x => x)
            .SetValidator(new BaseAgreementValidator());
    }
}