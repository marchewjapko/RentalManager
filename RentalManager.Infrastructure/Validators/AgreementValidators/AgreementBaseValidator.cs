using FluentValidation;
using RentalManager.Infrastructure.Commands.AgreementCommands;

namespace RentalManager.Infrastructure.Validators;

public class AgreementBaseValidator : AbstractValidator<AgreementBaseCommand>
{
    public AgreementBaseValidator()
    {
        RuleFor(x => x.EmployeeId)
            .NotEmpty();

        RuleFor(x => x.IsActive)
            .NotNull();

        RuleFor(x => x.ClientId)
            .NotEmpty();

        RuleFor(x => x.EquipmentIds)
            .NotEmpty()
            .Must(x => x.Count > 0)
            .WithMessage("'EquipmentIds' must contain at least one element")
            .Must(x => x.Count < 100);

        RuleFor(x => x.Comment)
            .Matches(@"^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻĆ 0-9\/.\/+\-\/%]*$")
            .When(x => x.Comment is not null);

        RuleFor(x => x.Deposit)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.TransportFromPrice)
            .GreaterThanOrEqualTo(0)
            .When(x => x.TransportFromPrice is not null);

        RuleFor(x => x.TransportToPrice)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);

        RuleForEach(x => x.Payments)
            .SetValidator(new PaymentBaseValidator());
    }
}