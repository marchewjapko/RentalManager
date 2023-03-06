namespace RentalManager.Infrastructure.DTO;

public class RentalAgreementDto
{
    public int Id { get; set; }
    public EmployeeDto Employee { get; set; } = null!;
    public bool IsActive { get; set; }
    public ClientDto Client { get; set; } = null!;
    public IEnumerable<RentalEquipmentDto> RentalEquipment { get; set; } = null!;
    public IEnumerable<PaymentDto> Payments { get; set; } = null!;
    public string? Comment { get; set; }
    public int Deposit { get; set; }
    public int? TransportFrom { get; set; }
    public int TransportTo { get; set; }
    public DateTime DateAdded { get; set; }
}