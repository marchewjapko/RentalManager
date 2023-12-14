namespace RentalManager.Infrastructure.Exceptions;

public class LoginFailedException : Exception
{
    public LoginFailedException(string userName) : base($"Login failed for user {userName}")
    {
    }
}