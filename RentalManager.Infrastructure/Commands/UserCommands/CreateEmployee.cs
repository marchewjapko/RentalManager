using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentalManager.Infrastructure.Commands.UserCommands;

public class CreateUser : UserBaseCommand
{
    [Required]
    [DefaultValue("123123")]
    public string Password { get; set; } = null!;

    [Required]
    [DefaultValue("JohnKowalski")]
    public string UserName { get; set; } = null!;
}