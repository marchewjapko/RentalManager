namespace RentalManager.Infrastructure.DTO;

public class EmployeeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public DateTime DateAdded { get; set; }
}