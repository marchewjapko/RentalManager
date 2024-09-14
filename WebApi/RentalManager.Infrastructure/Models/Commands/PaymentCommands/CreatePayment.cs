using System.ComponentModel.DataAnnotations;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace RentalManager.Infrastructure.Models.Commands.PaymentCommands;

public class CreatePayment : PaymentBaseCommand
{
    [Required]
    public int AgreementId { get; init; }
}