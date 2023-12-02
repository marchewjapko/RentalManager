using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentalManager.Infrastructure.Commands.ClientCommands;

public class CreateClient
{
    [DefaultValue("John")]
    [Required]
    public string Name { get; set; } = null!;

    [DefaultValue("Kowalski")]
    [Required]
    public string Surname { get; set; } = null!;

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
    [DefaultValue("Wiejska 4/6/8")]
    public string Street { get; set; } = null!;
}