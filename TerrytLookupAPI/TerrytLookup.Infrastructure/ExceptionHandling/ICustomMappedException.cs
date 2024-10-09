using Microsoft.AspNetCore.Mvc;

namespace TerrytLookup.Infrastructure.ExceptionHandling;

public interface ICustomMappedException
{
    public ProblemDetails GetProblemDetails(Exception exception);
}