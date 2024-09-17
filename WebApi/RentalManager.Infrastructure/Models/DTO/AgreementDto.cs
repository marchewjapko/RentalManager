namespace RentalManager.Infrastructure.Models.DTO;

public class AgreementDto
{
    public int Id { get; init; }

    public UserDto User { get; set; }

    public bool IsActive { get; init; }

    public ClientDto Client { get; init; }

    public IEnumerable<EquipmentDto> Equipments { get; init; }

    public IEnumerable<PaymentDto> Payments { get; init; }

    public string? Comment { get; init; }

    public int Deposit { get; init; }

    public int? TransportFromPrice { get; init; }

    public int TransportToPrice { get; init; }

    public DateTime DateAdded { get; init; }
}