namespace RentalManager.Infrastructure.DTO;

public class EmployeeDto
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public string Surname { get; init; } = null!;

    public GenderDto Gender { get; set; }

    public byte[]? Image { get; set; }

    public DateTime DateAdded { get; init; }
}