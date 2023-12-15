using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentalManager.Global.Requests;

public class LoginRequest
{
    [Required]
    [DefaultValue(true)]
    public bool UseCookies { get; set; } = true;

    [Required]
    [DefaultValue(true)]
    public bool UseSessionCookies { get; set; } = true;

    [Required]
    public string UserName { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}