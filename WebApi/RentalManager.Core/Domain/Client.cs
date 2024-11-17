using System.ComponentModel.DataAnnotations;

namespace RentalManager.Core.Domain;

public class Client : DomainBase
{
    [MaxLength(100)]
    public required string FirstName { get; set; }

    [MaxLength(100)]
    public required string LastName { get; set; }
    
    [MaxLength(15)]
    public required string PhoneNumber { get; set; }

    [MaxLength(100)]
    public string? Email { get; set; }

    [MaxLength(10)]
    public string? IdCard { get; set; }

    [MaxLength(100)]
    public required string City { get; set; }

    [MaxLength(100)]
    public string? Street { get; set; }
}