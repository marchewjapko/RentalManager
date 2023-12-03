namespace RentalManager.Infrastructure.Exceptions;

public class ClientNotFoundException : Exception
{
    public ClientNotFoundException(int id) : base($"Client with id {id} not found")
    {
    }
}