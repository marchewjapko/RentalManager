namespace RentalManager.Core.Domain;

public class RentalAgreement
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public bool IsActive { get; set; }
    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;
    public string? Comment { get; set; }
    public int Deposit { get; set; }
    public int? TransportFrom { get; set; }
    public int TransportTo { get; set; }
    public DateTime DateAdded { get; set; }

    public ICollection<RentalEquipment> RentalEquipment { get; set; } = null!;
    public ICollection<Payment> Payments { get; set; } = null!;
}