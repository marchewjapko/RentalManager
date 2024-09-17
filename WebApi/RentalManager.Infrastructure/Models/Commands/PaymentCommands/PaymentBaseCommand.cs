using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentalManager.Infrastructure.Models.Commands.PaymentCommands;

public class PaymentBaseCommand
{
    [Required]
    [DefaultValue("Cash")]
    public string Method { get; set; }

    [Required]
    [DefaultValue(100)]
    public int Amount { get; set; }

    [Required]
    [DefaultValue(typeof(DateTime), "2023-01-01 00:00:00")]
    public DateTime DateFrom { get; set; }

    [Required]
    [DefaultValue(typeof(DateTime), "2023-02-01 00:00:00")]
    public DateTime DateTo { get; set; }
}