using System.ComponentModel.DataAnnotations;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace RentalManager.Global.Requests;

public class ResetPasswordRequest
{
    [Required]
    public string UserName { get; init; } = null!;

    [Required]
    public string NewPassword { get; init; } = null!;
}