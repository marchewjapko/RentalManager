using FluentValidation;
using RentalManager.Infrastructure.Commands.PaymentCommands;

namespace RentalManager.Infrastructure.Services.Validators;

public class PaymentValidator : AbstractValidator<PaymentBaseCommand>
{
    public PaymentValidator()
    {
        RuleFor(x => x.Method)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Amount)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.DateTimeTo)
            .NotNull()
            .NotEmpty()
            .GreaterThan(x => x.DateTimeFrom);

        RuleFor(x => x.DateTimeFrom)
            .NotNull()
            .NotEmpty();
    }
}