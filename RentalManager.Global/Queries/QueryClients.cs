using RentalManager.Global.Queries.Sorting;

namespace RentalManager.Global.Queries;

public class QueryClients : PagedQueryBase
{
    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Email { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    public DateTime? AddedFrom { get; set; }

    public DateTime? AddedTo { get; set; }

    public bool OnlyActive { get; set; } = true;

    public SortClientsBy SortClientsBy { get; set; }
}