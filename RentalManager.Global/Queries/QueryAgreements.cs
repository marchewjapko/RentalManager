using RentalManager.Global.Queries.Sorting;

namespace RentalManager.Global.Queries;

public class QueryAgreements : PagedQueryBase
{
    public string? City { get; set; }

    public int? ClientId { get; set; }

    public bool OnlyUnpaid { get; set; } = false;

    public string? Street { get; set; }

    public string? Surname { get; set; }

    public DateTime? AddedFrom { get; set; }

    public DateTime? AddedTo { get; set; }

    public int? EmployeeId { get; set; }

    public bool OnlyActive { get; set; } = true;

    public SortAgreementsBy SortAgreementsBy { get; set; } = SortAgreementsBy.DateAdded;
}