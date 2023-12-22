namespace RentalManager.Infrastructure.Exceptions;

public class UserNotConfirmedException(string userName) : Exception(
    $"User {userName} has not been activated");