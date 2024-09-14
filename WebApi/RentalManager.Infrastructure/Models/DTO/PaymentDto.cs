namespace RentalManager.Infrastructure.Models.DTO;

public class PaymentDto
{
    public int Id { get; init; }

    public string? Method { get; init; }

    public int Amount { get; init; }

    public DateTime DateFrom { get; init; }

    public DateTime DateTo { get; init; }
}