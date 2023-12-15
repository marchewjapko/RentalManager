using FluentValidation;
using RentalManager.Infrastructure.Commands.AgreementCommands;

namespace RentalManager.Infrastructure.Validators.AgreementValidators;

public class CreateAgreementValidator : AbstractValidator<CreateAgreement>
{
    public CreateAgreementValidator()
    {
        RuleFor(x => x)
            .SetValidator(new AgreementBaseValidator());
    }
}