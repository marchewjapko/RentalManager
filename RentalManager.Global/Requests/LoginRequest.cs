using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace RentalManager.Global.Requests;

public class LoginRequest
{
    [Required]
    [DefaultValue(true)]
    public bool UseCookies { get; init; } = true;

    [Required]
    [DefaultValue(true)]
    public bool UseSessionCookies { get; init; } = true;

    [Required]
    public string UserName { get; init; } = null!;

    [Required]
    public string Password { get; init; } = null!;
}