using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace RentalManager.Infrastructure.Commands.UserCommands;

public class CreateUser : UserBaseCommand
{
    [Required]
    [DefaultValue("123123")]
    public string Password { get; init; } = null!;

    [Required]
    [DefaultValue("JohnKowalski")]
    public string UserName { get; init; } = null!;
}