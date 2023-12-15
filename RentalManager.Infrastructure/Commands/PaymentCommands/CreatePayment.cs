using System.ComponentModel.DataAnnotations;

namespace RentalManager.Infrastructure.Commands.PaymentCommands;

public class CreatePayment : PaymentBaseCommand
{
    [Required]
    public int AgreementId { get; set; }
}