namespace RentalManager.Infrastructure.DTO;

public class ClientDto
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public string Surname { get; init; } = null!;
    public string PhoneNumber { get; init; } = null!;
    public string? Email { get; init; }
    public string? IdCard { get; init; }
    public string City { get; init; } = null!;
    public string Street { get; init; } = null!;
    public DateTime DateAdded { get; init; }
}