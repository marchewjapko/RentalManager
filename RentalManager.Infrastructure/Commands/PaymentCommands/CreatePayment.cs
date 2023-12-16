using System.ComponentModel.DataAnnotations;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace RentalManager.Infrastructure.Commands.PaymentCommands;

public class CreatePayment : PaymentBaseCommand
{
    [Required]
    public int AgreementId { get; init; }
}