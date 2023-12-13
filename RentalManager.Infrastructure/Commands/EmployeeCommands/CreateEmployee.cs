using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentalManager.Infrastructure.Commands.EmployeeCommands;

public class CreateEmployee : EmployeeBaseCommand
{
    [Required]
    public string Password { get; set; } = null!;

    [Required]
    [DefaultValue("JohnKowalski")]
    public string UserName { get; set; } = null!;
}