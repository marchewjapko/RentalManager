namespace RentalManager.Infrastructure.Models.DTO;

public class UserDto
{
    public int Id { get; init; }

    public string? FirstName { get; init; }

    public string? LastName { get; init; }

    public string UserName { get; set; } = null!;
}