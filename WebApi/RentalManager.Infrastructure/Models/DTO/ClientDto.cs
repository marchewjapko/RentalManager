namespace RentalManager.Infrastructure.Models.DTO;

public class ClientDto
{
    public int Id { get; init; }

    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required string PhoneNumber { get; init; }

    public string? Email { get; init; }

    public string? IdCard { get; init; }

    public required string City { get; init; }

    public string? Street { get; init; }
}