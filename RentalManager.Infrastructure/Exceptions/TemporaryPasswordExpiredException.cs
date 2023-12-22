namespace RentalManager.Infrastructure.Exceptions;

public class TemporaryPasswordExpiredException(string userName) : Exception(
    $"Temporary password for user {userName} has expired");