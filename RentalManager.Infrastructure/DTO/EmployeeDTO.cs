namespace RentalManager.Infrastructure.DTO;

public class EmployeeDto
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public string Surname { get; init; } = null!;
    public DateTime DateAdded { get; init; }
}