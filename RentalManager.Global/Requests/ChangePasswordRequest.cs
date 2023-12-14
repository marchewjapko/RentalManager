using System.ComponentModel.DataAnnotations;

namespace RentalManager.Infrastructure.Requests;

public class ChangePasswordRequest
{
    [Required]
    public string UserName { get; set; } = null!;

    [Required]
    public string OldPassword { get; set; } = null!;

    [Required]
    public string NewPassword { get; set; } = null!;
}