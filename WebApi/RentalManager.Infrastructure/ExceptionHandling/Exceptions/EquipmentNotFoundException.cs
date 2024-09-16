using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RentalManager.Infrastructure.ExceptionHandling.Exceptions;

public class EquipmentNotFoundException : Exception, ICustomMappedException
{
    public EquipmentNotFoundException(int id) : base($"Equipment with id {id} not found")
    {
    }

    public EquipmentNotFoundException(IEnumerable<int> ids) : base(
        $"Equipment with ids: {string.Join(", ", ids)} not found")
    {
    }

    public ProblemDetails GetProblemDetails(Exception exception)
    {
        return new ProblemDetails
        {
            Title = "Equipment not found",
            Detail = exception.Message,
            Status = StatusCodes.Status404NotFound
        };
    }
}