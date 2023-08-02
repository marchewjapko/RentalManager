using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentalManager.Infrastructure.Commands.EmployeeCommands;

public class CreateEmployee
{
    [Required]
    [RegularExpression(@"^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻ]*$", ErrorMessage = "Invalid name")]
    [StringLength(100)]
    [DefaultValue("John")]
    public string Name { get; set; } = null!;

    [Required]
    [RegularExpression(@"^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻ]*$", ErrorMessage = "Invalid surname")]
    [StringLength(100)]
    [DefaultValue("Kowalski")]
    public string Surname { get; set; } = null!;
}