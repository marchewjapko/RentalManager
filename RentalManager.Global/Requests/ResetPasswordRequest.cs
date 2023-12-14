using System.ComponentModel.DataAnnotations;

namespace RentalManager.Infrastructure.Requests;

public class ResetPasswordRequest
{
    [Required]
    public string UserName { get; set; } = null!;

    [Required]
    public string NewPassword { get; set; } = null!;
}