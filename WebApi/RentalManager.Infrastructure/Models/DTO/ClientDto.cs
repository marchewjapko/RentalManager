namespace RentalManager.Infrastructure.Models.DTO;

public class ClientDto
{
    public int Id { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string PhoneNumber { get; init; }

    public string? Email { get; init; }

    public string? IdCard { get; init; }

    public string City { get; init; }

    public string Street { get; init; }
}