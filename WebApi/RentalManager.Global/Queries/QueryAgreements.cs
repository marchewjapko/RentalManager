using RentalManager.Global.Queries.Sorting;

namespace RentalManager.Global.Queries;

public class QueryAgreements : PagedQueryBase
{
    public string? City { get; init; }

    public int? ClientId { get; init; }

    public bool OnlyUnpaid { get; init; } = false;

    public string? Street { get; init; }

    public string? Surname { get; init; }

    public DateTime? AddedFrom { get; init; }

    public DateTime? AddedTo { get; init; }

    public int? UserId { get; init; }

    public bool OnlyActive { get; init; } = true;

    public SortAgreementsBy SortAgreementsBy { get; init; } = SortAgreementsBy.DateAdded;
}