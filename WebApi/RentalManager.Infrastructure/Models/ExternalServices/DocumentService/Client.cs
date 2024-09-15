// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace RentalManager.Infrastructure.Models.ExternalServices.DocumentService;

public class Client
{
    public string Name { get; init; } = null!;

    public string Surname { get; init; } = null!;

    public string Address { get; init; } = null!;

    public string PhoneNumber { get; init; } = null!;

    public string? IdCard { get; init; }
}