using FluentValidation;
using RentalManager.Infrastructure.Commands.PaymentCommands;

namespace RentalManager.Infrastructure.Validators;

public class UpdatePaymentValidator : AbstractValidator<CreatePayment>
{
    public UpdatePaymentValidator()
    {
        RuleFor(x => x)
            .SetValidator(new PaymentBaseValidator());
    }
}