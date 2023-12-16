namespace RentalManager.Infrastructure.DTO;

public class UserDto
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public string Surname { get; init; } = null!;

    public string UserName { get; set; } = null!;
    
    public byte[]? Image { get; set; }
}