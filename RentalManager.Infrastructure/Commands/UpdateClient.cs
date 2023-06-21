namespace RentalManager.Infrastructure.Commands;

public class UpdateClient
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string? Email { get; set; } = null!;
    public string? IdCard { get; set; }
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
}