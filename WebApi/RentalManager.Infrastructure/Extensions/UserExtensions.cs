using System.Security.Claims;
using RentalManager.Infrastructure.ExceptionHandling.Exceptions;

namespace RentalManager.Infrastructure.Extensions;

public static class UserExtensions
{
    public static int GetId(this ClaimsPrincipal principal)
    {
        var claimStr = principal.Claims.FirstOrDefault(x => x.Type == "id");
        if (claimStr is null)
        {
            throw new UserDoesNotHaveIdClaimException();
        }

        try
        {
            return Convert.ToInt32(claimStr.Value);
        }
        catch
        {
            throw new UserIdClaimInvalidException(claimStr.Value);
        }
    }
}