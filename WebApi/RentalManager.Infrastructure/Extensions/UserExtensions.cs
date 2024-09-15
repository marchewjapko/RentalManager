using System.Security.Claims;

namespace RentalManager.Infrastructure.Extensions;

public static class UserExtensions
{
    public static int GetId(this ClaimsPrincipal principal)
    {
        return Convert.ToInt32(principal.Claims.FirstOrDefault(c => c.Type == "id")
            ?.Value);
    }
}