using FluentValidation;
using RentalManager.Infrastructure.Commands.AgreementCommands;

namespace RentalManager.Infrastructure.Validators;

public class UpdateAgreementValidator  : AbstractValidator<CreateAgreement>
{
    public UpdateAgreementValidator()
    {
        RuleFor(x => x)
            .SetValidator(new AgreementBaseValidator());
    }
}