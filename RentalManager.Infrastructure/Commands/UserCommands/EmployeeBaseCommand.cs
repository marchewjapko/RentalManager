using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using RentalManager.Infrastructure.DTO;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace RentalManager.Infrastructure.Commands.UserCommands;

public class UserBaseCommand
{
    [Required]
    [DefaultValue("John")]
    public string Name { get; init; } = null!;

    [Required]
    [DefaultValue("Kowalski")]
    public string Surname { get; init; } = null!;

    public IFormFile? Image { get; init; }
}