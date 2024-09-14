using FluentValidation;
using RentalManager.Infrastructure.Models.Commands.AgreementCommands;

namespace RentalManager.Infrastructure.Validators.AgreementValidators;

public class CreateAgreementValidator : AbstractValidator<CreateAgreement>
{
    public CreateAgreementValidator()
    {
        RuleFor(x => x)
            .SetValidator(new AgreementBaseValidator());
    }
}