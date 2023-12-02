namespace RentalManager.Core.Domain;

public class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public DateTime DateAdded { get; set; }

    public virtual ICollection<RentalAgreement> RentalAgreements { get; set; }
}