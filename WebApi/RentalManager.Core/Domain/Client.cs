namespace RentalManager.Core.Domain;

public class Client : DomainBase
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? IdCard { get; set; }

    public string City { get; set; }

    public string Street { get; set; }
}