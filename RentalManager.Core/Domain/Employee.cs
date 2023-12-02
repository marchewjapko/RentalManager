namespace RentalManager.Core.Domain;

public class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public Gender Gender { get; set; }

    public byte[]? Image { get; set; }

    public DateTime DateAdded { get; set; }
}