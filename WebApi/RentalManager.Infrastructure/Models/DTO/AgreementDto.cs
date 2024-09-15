namespace RentalManager.Infrastructure.Models.DTO;

public class AgreementDto
{
    public int Id { get; init; }

    public UserDto User { get; set; } = null!;

    public bool IsActive { get; init; }

    public ClientDto Client { get; init; } = null!;

    public IEnumerable<EquipmentDto> Equipments { get; init; } = null!;

    public IEnumerable<PaymentDto> Payments { get; init; } = null!;

    public string? Comment { get; init; }

    public int Deposit { get; init; }

    public int? TransportFrom { get; init; }

    public int TransportTo { get; init; }

    public DateTime DateAdded { get; init; }
}