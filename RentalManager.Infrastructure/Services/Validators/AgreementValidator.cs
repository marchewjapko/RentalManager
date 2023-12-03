using FluentValidation;
using RentalManager.Infrastructure.Commands.AgreementCommands;

namespace RentalManager.Infrastructure.Services.Validators;

public class AgreementValidator : AbstractValidator<AgreementBaseCommand>
{
    public AgreementValidator()
    {
        RuleFor(x => x.EmployeeId)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.IsActive)
            .NotNull();

        RuleFor(x => x.ClientId)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.EquipmentIds)
            .NotNull()
            .Must(x => x.Count > 0)
            .WithMessage("'EquipmentIds' must contain at least one element")
            .Must(x => x.Count < 100);

        RuleFor(x => x.Comment)
            .NotEmpty()
            .Matches(@"^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻ 0-9\/.\/+\-\/%]*$");

        RuleFor(x => x.Deposit)
            .NotNull()
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.TransportFromPrice)
            .GreaterThanOrEqualTo(0)
            .When(x => x.TransportFromPrice is not null);

        RuleFor(x => x.TransportToPrice)
            .NotNull()
            .GreaterThanOrEqualTo(0);

        RuleForEach(x => x.Payments)
            .SetValidator(new PaymentValidator());
    }
}