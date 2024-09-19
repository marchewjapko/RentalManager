using System.ComponentModel.DataAnnotations;

namespace RentalManager.Infrastructure.Models.Commands.PaymentCommands;

public class CreatePaymentCommand : BasePaymentCommand
{
    [Required]
    public int AgreementId { get; init; }
}