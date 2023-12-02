using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentalManager.Infrastructure.Commands.PaymentCommands;

public class UpdatePayment
{
    [RegularExpression(@"^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻ]*$", ErrorMessage = "Invalid payment method")]
    [StringLength(10)]
    [DefaultValue("Cash")]
    public string? Method { get; set; }

    [DefaultValue(100)]
    [Range(0, int.MaxValue)]
    public int Amount { get; set; }

    public DateTime From { get; set; }

    public DateTime To { get; set; }
}