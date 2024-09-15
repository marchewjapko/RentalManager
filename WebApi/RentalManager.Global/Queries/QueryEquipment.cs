using RentalManager.Global.Queries.Sorting;

namespace RentalManager.Global.Queries;

public class QueryEquipment : PagedQueryBase
{
    public string? Name { get; init; }

    public DateTime? AddedFrom { get; init; }

    public DateTime? AddedTo { get; init; }

    public bool OnlyActive { get; init; } = true;

    public SortEquipmentBy SortEquipmentBy { get; init; } = SortEquipmentBy.DateAdded;
}