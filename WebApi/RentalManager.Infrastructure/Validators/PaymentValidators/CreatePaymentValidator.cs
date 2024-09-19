using FluentValidation;
using RentalManager.Infrastructure.Models.Commands.PaymentCommands;

namespace RentalManager.Infrastructure.Validators.PaymentValidators;

public class CreatePaymentValidator : AbstractValidator<CreatePaymentCommand>
{
    public CreatePaymentValidator()
    {
        RuleFor(x => x)
            .SetValidator(new BasePaymentValidator());
    }
}