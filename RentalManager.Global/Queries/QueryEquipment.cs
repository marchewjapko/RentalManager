namespace RentalManager.Global.Queries;

public class QueryEquipment
{
    public string? Name { get; set; }

    public DateTime? From { get; set; }

    public DateTime? To { get; set; }

    public bool OnlyActive { get; set; } = true;
}