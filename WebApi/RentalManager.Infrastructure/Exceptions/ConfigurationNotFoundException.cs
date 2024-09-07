namespace RentalManager.Infrastructure.Exceptions;

public class ConfigurationNotFoundException(string key) : Exception(
    $"Configuration with key {key} not found");