using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentalManager.Infrastructure.Commands.PaymentCommands;

public class CreatePayment
{
    [DefaultValue("Cash")]
    public string? Method { get; set; }

    [DefaultValue(100)]
    public int Amount { get; set; }

    public DateTime From { get; set; }

    public DateTime To { get; set; }
}