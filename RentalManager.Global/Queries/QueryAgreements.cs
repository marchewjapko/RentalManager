namespace RentalManager.Global.Queries;

public class QueryAgreements
{
    public string? City { get; set; }

    public int? ClientId { get; set; }

    public DateTime? From { get; set; }

    public bool OnlyUnpaid { get; set; } = false;

    public string? PhoneNumber { get; set; }

    public int? EquipmentId { get; set; }

    public string? EquipmentName { get; set; }

    public string? Street { get; set; }

    public string? Surname { get; set; }

    public DateTime? To { get; set; }

    public int? EmployeeId { get; set; }

    public bool OnlyActive { get; set; } = true;
}