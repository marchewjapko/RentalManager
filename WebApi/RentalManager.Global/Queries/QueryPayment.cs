namespace RentalManager.Global.Queries;

public class QueryPayment
{
    public int? AgreementId { get; init; }

    public string? Method { get; init; }

    public DateTime? From { get; init; }

    public DateTime? To { get; init; }

    public bool OnlyActive { get; init; } = true;
}