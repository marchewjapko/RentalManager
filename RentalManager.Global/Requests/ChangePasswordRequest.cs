using System.ComponentModel.DataAnnotations;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace RentalManager.Global.Requests;

public class ChangePasswordRequest
{
    [Required]
    public string UserName { get; init; } = null!;

    [Required]
    public string OldPassword { get; init; } = null!;

    [Required]
    public string NewPassword { get; init; } = null!;
}