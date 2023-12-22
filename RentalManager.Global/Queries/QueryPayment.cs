// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace RentalManager.Global.Queries;

public class QueryPayment
{
    public int? AgreementId { get; init; }

    public string? Method { get; init; } = null!;

    public DateTime? From { get; init; }

    public DateTime? To { get; init; }

    public bool OnlyActive { get; init; } = true;
}