using Microsoft.AspNetCore.Identity;

namespace RentalManager.Infrastructure.Extensions;

public static class IdentityErrorExtensions
{
    public static string ToString(this IdentityError identityError)
    {
        return "DUPA";
    }

    public static string ToString(this IEnumerable<IdentityError> identityErrors)
    {
        return "DUPAAA";
    }
}