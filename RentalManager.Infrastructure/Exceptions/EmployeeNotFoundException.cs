namespace RentalManager.Infrastructure.Exceptions;

public class EmployeeNotFoundException : Exception
{
    public EmployeeNotFoundException(int id) : base($"Employee with id {id} not found")
    {
    }
}