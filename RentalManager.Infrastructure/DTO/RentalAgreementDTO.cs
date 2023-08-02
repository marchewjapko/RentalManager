namespace RentalManager.Infrastructure.DTO;

public class RentalAgreementDto
{
    public int Id { get; init; }
    public EmployeeDto Employee { get; init; } = null!;
    public bool IsActive { get; init; }
    public ClientDto Client { get; init; } = null!;
    public IEnumerable<RentalEquipmentDto> RentalEquipment { get; init; } = null!;
    public IEnumerable<PaymentDto> Payments { get; init; } = null!;
    public string? Comment { get; init; }
    public int Deposit { get; init; }
    public int? TransportFrom { get; init; }
    public int TransportTo { get; init; }
    public DateTime DateAdded { get; init; }
}