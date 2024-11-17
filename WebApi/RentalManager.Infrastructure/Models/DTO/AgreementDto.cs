namespace RentalManager.Infrastructure.Models.DTO;

public class AgreementDto
{
    public int Id { get; init; }

    public required UserDto User { get; set; }

    public bool IsActive { get; init; }

    public required ClientDto Client { get; init; }

    public required IEnumerable<EquipmentDto> Equipments { get; init; }

    public required IEnumerable<PaymentDto> Payments { get; init; }

    public string? Comment { get; init; }

    public int Deposit { get; init; }

    public int? TransportFromPrice { get; init; }

    public int TransportToPrice { get; init; }

    public DateTime DateAdded { get; init; }
}