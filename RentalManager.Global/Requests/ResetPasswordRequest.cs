using System.ComponentModel.DataAnnotations;

namespace RentalManager.Global.Requests;

public class ResetPasswordRequest
{
    [Required]
    public string UserName { get; set; } = null!;

    [Required]
    public string NewPassword { get; set; } = null!;
}