using FluentValidation;
using RentalManager.Infrastructure.Commands.PaymentCommands;

namespace RentalManager.Infrastructure.Validators.PaymentValidators;

public class UpdatePaymentValidator : AbstractValidator<UpdatePayment>
{
    public UpdatePaymentValidator()
    {
        RuleFor(x => x)
            .SetValidator(new PaymentBaseValidator());
    }
}