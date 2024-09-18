using RentalManager.Global.Queries.Sorting;

namespace RentalManager.Global.Queries;

public class QueryClients : PagedQueryBase
{
    public string? FirstName { get; init; }

    public string? LastName { get; init; }

    public string? Email { get; init; }

    public string? City { get; init; }

    public string? Street { get; init; }

    public DateTime? AddedFrom { get; init; }

    public DateTime? AddedTo { get; init; }

    public bool OnlyActive { get; init; } = true;

    public SortClientsBy SortClientsBy { get; init; } = SortClientsBy.DateAdded;
}