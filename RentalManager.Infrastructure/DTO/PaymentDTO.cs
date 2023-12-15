namespace RentalManager.Infrastructure.DTO;

public class PaymentDto
{
    public int Id { get; init; }

    public string? Method { get; init; }

    public int Amount { get; init; }

    public DateTime From { get; init; }

    public DateTime To { get; init; }
}