namespace RentalManager.Infrastructure.Exceptions;

public class PasswordChangeFailedException(string userName) : Exception(
    $"Password change for user {userName} failed");