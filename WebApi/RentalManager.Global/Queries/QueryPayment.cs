namespace RentalManager.Global.Queries;

public class QueryPayment
{
    public int? AgreementId { get; init; }

    public string? Method { get; init; }

    public DateTime? ValidRangeFrom { get; init; }

    public DateTime? ValidRangeTo { get; init; }

    public bool OnlyActive { get; init; } = true;
}