namespace RentalManager.Infrastructure.Exceptions;

public class UserNotConfirmedException : Exception
{
    public UserNotConfirmedException(string userName) : base(
        $"User {userName} has not been activated")
    {
    }
}