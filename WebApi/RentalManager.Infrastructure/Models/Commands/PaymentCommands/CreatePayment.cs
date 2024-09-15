using System.ComponentModel.DataAnnotations;

namespace RentalManager.Infrastructure.Models.Commands.PaymentCommands;

public class CreatePayment : PaymentBaseCommand
{
    [Required]
    public int AgreementId { get; init; }
}