using FluentValidation;
using RentalManager.Infrastructure.Commands.PaymentCommands;

namespace RentalManager.Infrastructure.Validators;

public class PaymentValidator : AbstractValidator<PaymentBaseCommand>
{
    public PaymentValidator()
    {
        RuleFor(x => x.Method)
            .NotEmpty()
            .MaximumLength(10)
            .Matches("^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻĆ ']*$");

        RuleFor(x => x.Amount)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.DateTimeTo)
            .NotEmpty()
            .GreaterThan(x => x.DateTimeFrom);

        RuleFor(x => x.DateTimeFrom)
            .NotEmpty();
    }
}