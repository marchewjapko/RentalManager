using FluentValidation;
using RentalManager.Infrastructure.Models.Commands.PaymentCommands;

namespace RentalManager.Infrastructure.Validators.PaymentValidators;

public class UpdatePaymentValidator : AbstractValidator<UpdatePaymentCommand>
{
    public UpdatePaymentValidator()
    {
        RuleFor(x => x)
            .SetValidator(new BasePaymentValidator());
    }
}