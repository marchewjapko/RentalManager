using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace RentalManager.Infrastructure.Commands.UserCommands;

public class UserBaseCommand
{
    [Required]
    [DefaultValue("John")]
    public string Name { get; set; } = null!;

    [Required]
    [DefaultValue("Kowalski")]
    public string Surname { get; set; } = null!;

    public IFormFile? Image { get; set; }
}