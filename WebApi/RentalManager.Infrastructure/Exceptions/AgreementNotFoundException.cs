namespace RentalManager.Infrastructure.Exceptions;

public class AgreementNotFoundException : Exception
{
    public AgreementNotFoundException(int id) : base($"Agreement with id {id} not found")
    {
    }
}