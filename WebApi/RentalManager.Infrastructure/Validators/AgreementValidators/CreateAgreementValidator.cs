using FluentValidation;
using RentalManager.Infrastructure.Models.Commands.AgreementCommands;
using RentalManager.Infrastructure.Validators.PaymentValidators;

namespace RentalManager.Infrastructure.Validators.AgreementValidators;

public class CreateAgreementValidator : AbstractValidator<CreateAgreementCommand>
{
    public CreateAgreementValidator()
    {
        RuleFor(x => x)
            .SetValidator(new BaseAgreementValidator());

        RuleForEach(x => x.Payments)
            .SetValidator(new BasePaymentValidator());
    }
}