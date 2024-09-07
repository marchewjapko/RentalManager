namespace RentalManager.Infrastructure.Exceptions;

public class SignInFailedException
    (string userName) : Exception($"Login failed for user {userName}");