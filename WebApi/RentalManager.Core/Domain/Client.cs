namespace RentalManager.Core.Domain;

public class Client : DomainBase
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? Email { get; set; }

    public string? IdCard { get; set; }

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;
}