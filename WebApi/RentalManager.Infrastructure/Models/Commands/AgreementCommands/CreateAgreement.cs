using RentalManager.Infrastructure.Models.Commands.PaymentCommands;

namespace RentalManager.Infrastructure.Models.Commands.AgreementCommands;

public class CreateAgreement : AgreementBaseCommand
{
    public List<CreatePayment> Payments { get; init; } = null!;
}