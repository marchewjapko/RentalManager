namespace RentalManager.Infrastructure.Exceptions;

public class PasswordChangeFailedException : Exception
{
    public PasswordChangeFailedException(string userName) : base(
        $"Password change for user {userName} failed")
    {
    }
}