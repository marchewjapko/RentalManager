namespace RentalManager.Infrastructure.Exceptions;

public class ConfigurationNotFoundException : Exception
{
    public ConfigurationNotFoundException(string key) : base(
        $"Configuration with key {key} not found")
    {
    }
}