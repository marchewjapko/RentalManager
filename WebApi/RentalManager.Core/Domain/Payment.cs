using System.ComponentModel.DataAnnotations;

namespace RentalManager.Core.Domain;

public class Payment : DomainBase
{
    public int AgreementId { get; set; }

    public required Agreement Agreement { get; set; }

    [MaxLength(20)]
    public string? Method { get; set; }

    public int Amount { get; set; }

    public DateTime DateFrom { get; set; }

    public DateTime DateTo { get; set; }
}