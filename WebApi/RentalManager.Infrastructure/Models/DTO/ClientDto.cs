namespace RentalManager.Infrastructure.Models.DTO;

public class ClientDto
{
    public int Id { get; init; }

    public string FirstName { get; init; } = null!;

    public string LastName { get; init; } = null!;

    public string PhoneNumber { get; init; } = null!;

    public string? Email { get; init; }

    public string? IdCard { get; init; }

    public string City { get; init; } = null!;

    public string Street { get; init; } = null!;
}