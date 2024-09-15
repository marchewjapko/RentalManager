using FluentValidation;
using RentalManager.Infrastructure.Models.Commands.AgreementCommands;
using RentalManager.Infrastructure.Validators.PaymentValidators;

namespace RentalManager.Infrastructure.Validators.AgreementValidators;

public class CreateAgreementValidator : AbstractValidator<CreateAgreement>
{
    public CreateAgreementValidator()
    {
        RuleFor(x => x)
            .SetValidator(new AgreementBaseValidator());

        RuleForEach(x => x.Payments)
            .SetValidator(new PaymentBaseValidator());
    }
}