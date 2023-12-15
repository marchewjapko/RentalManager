using FluentValidation;
using RentalManager.Infrastructure.Commands.PaymentCommands;

namespace RentalManager.Infrastructure.Validators.PaymentValidators;

public class CreatePaymentValidator : AbstractValidator<CreatePayment>
{
    public CreatePaymentValidator()
    {
        RuleFor(x => x)
            .SetValidator(new PaymentBaseValidator());
    }
}