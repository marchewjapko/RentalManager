using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace RentalManager.Global.Requests;

public class SignInRequest
{
    [Required]
    [DefaultValue(true)]
    public bool IsPersistent { get; init; } = true;

    [Required]
    public string UserName { get; init; } = null!;

    [Required]
    public string Password { get; init; } = null!;
}