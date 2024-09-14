using FluentValidation;
using RentalManager.Infrastructure.Models.Commands.PaymentCommands;

namespace RentalManager.Infrastructure.Validators.PaymentValidators;

public class PaymentBaseValidator : AbstractValidator<PaymentBaseCommand>
{
    public PaymentBaseValidator()
    {
        RuleFor(x => x.Method)
            .NotEmpty()
            .MaximumLength(10)
            .Matches("^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻĆ ']*$");

        RuleFor(x => x.Amount)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.DateTo)
            .NotEmpty()
            .GreaterThan(x => x.DateFrom);

        RuleFor(x => x.DateFrom)
            .NotEmpty();
    }
}