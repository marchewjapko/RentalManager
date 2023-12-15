using System.ComponentModel.DataAnnotations;

namespace RentalManager.Global.Queries;

public class QueryPayment
{
    public int? AgreementId { get; set; }
    
    public string? Method { get; set; } = null!;
    
    public DateTime? From { get; set; }
    
    public DateTime? To { get; set; }

    public bool OnlyActive { get; set; } = true;
}