// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace RentalManager.Global.Queries;

public class QueryUser
{
    public string? Name { get; init; }

    public DateTime? From { get; init; }

    public DateTime? To { get; init; }
}