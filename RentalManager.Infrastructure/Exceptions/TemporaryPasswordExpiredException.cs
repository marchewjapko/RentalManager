namespace RentalManager.Infrastructure.Exceptions;

public class TemporaryPasswordExpiredException : Exception
{
    public TemporaryPasswordExpiredException(string userName) : base($@"Temporary password for user {userName} has expired")
    {
    }
}