namespace RentalManager.Infrastructure.Exceptions;

public class PaymentNotFoundException : Exception
{
    public PaymentNotFoundException(int id) : base($"Payment with id {id} not found")
    {
    }
}