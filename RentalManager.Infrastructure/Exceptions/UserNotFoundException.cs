namespace RentalManager.Infrastructure.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(int id) : base($"User with id {id} not found")
    {
    }

    public UserNotFoundException(string userName) : base($"User {userName} not found")
    {
    }
}