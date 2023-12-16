using RentalManager.Global.Queries.Sorting;

namespace RentalManager.Global.Queries;

public class QueryEquipment : PagedQueryBase
{
    public string? Name { get; set; }

    public DateTime? AddedFrom { get; set; }

    public DateTime? AddedTo { get; set; }

    public bool OnlyActive { get; set; } = true;

    public SortEquipmentBy SortEquipmentBy { get; set; }
}