namespace RentalManager.Infrastructure.Exceptions;

public class PasswordChangeRequiredException(string userName) : Exception(
    $"Password change for user {userName} is required");