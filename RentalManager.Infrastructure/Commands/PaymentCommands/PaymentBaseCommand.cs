using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentalManager.Infrastructure.Commands.PaymentCommands;

public class PaymentBaseCommand
{
    [Required]
    [DefaultValue("Cash")]
    public string Method { get; set; } = null!;

    [Required]
    [DefaultValue(100)]
    public int Amount { get; set; }

    [Required]
    [DefaultValue(typeof(DateTime), "2023-01-01 00:00:00")]
    public DateTime DateTimeFrom { get; set; }

    [Required]
    [DefaultValue(typeof(DateTime), "2023-02-01 00:00:00")]
    public DateTime DateTimeTo { get; set; }
}