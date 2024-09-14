using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentalManager.Infrastructure.Models.Commands.ClientCommands;

public class ClientBaseCommand

{
    [Required]
    [DefaultValue("John")]
    public string FirstName { get; set; } = null!;

    [Required]
    [DefaultValue("Kowalski")]
    public string LastName { get; set; } = null!;

    [Required]
    [DefaultValue("+48 123 123 123")]
    public string PhoneNumber { get; set; } = null!;

    [DefaultValue("JohnKowalski@email.com")]
    public string? Email { get; set; }

    [DefaultValue("ABC 123456")]
    public string? IdCard { get; set; }

    [Required]
    [DefaultValue("Warsaw")]
    public string City { get; set; } = null!;

    [Required]
    [DefaultValue("Woodland Ave 4/6/8")]
    public string Street { get; set; } = null!;
}