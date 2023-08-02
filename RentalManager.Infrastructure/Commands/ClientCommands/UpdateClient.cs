using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentalManager.Infrastructure.Commands.ClientCommands;

public class UpdateClient
{
    [DefaultValue("John")] [Required] public string Name { get; set; } = null!;

    [DefaultValue("Kowalski")] [Required] public string Surname { get; set; } = null!;

    [Required]
    [DefaultValue("+48 123 123 123")]
    [RegularExpression(
        @"^((\+[0-9]{1,4})?[-. ]?([0-9]{3})[-. ]?([0-9]{3})[-. ]?([0-9]{3}))$|^(\(?[0-9]{2}\)?[-. ]?[0-9]{2}[-. ]?[0-9]{3}[-. ]?[0-9]{2})$",
        ErrorMessage = "Invalid phone number")]
    public string PhoneNumber { get; set; } = null!;

    [RegularExpression(@"^[a-zA-Z]*\@[a-zA-Z]{3,}\.[a-zA-Z]{2,5}$", ErrorMessage = "Invalid email address")]
    [StringLength(100)]
    [DefaultValue("JohnKowalski@email.com")]
    public string? Email { get; set; }

    [RegularExpression(@"^([a-zA-Z]){3}[ ]?[0-9]{6}$", ErrorMessage = "Invalid ID card information")]
    [DefaultValue("ABC 123456")]
    public string? IdCard { get; set; }

    [Required]
    [RegularExpression(@"^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻ]*$", ErrorMessage = "Invalid city")]
    [StringLength(100)]
    [DefaultValue("Warsaw")]
    public string City { get; set; } = null!;

    [Required]
    [RegularExpression(@"^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻ 0-9\/.]*$", ErrorMessage = "Invalid street")]
    [StringLength(100)]
    [DefaultValue("Wiejska 4/6/8")]
    public string Street { get; set; } = null!;
}