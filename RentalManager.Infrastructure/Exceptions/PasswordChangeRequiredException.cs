namespace RentalManager.Infrastructure.Exceptions;

public class PasswordChangeRequiredException : Exception
{
    public PasswordChangeRequiredException(string userName) : base(
        $"Password change for user {userName} is required")
    {
    }
}