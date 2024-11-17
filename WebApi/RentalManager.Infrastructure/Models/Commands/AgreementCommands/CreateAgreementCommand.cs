using RentalManager.Infrastructure.Models.Commands.PaymentCommands;

namespace RentalManager.Infrastructure.Models.Commands.AgreementCommands;

public class CreateAgreementCommand : BaseAgreementCommand
{
    public required IEnumerable<CreatePaymentCommand> Payments { get; init; }
}