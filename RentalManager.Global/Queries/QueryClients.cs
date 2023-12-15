namespace RentalManager.Global.Queries;

public class QueryClients
{
    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? IdCard { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    public DateTime? From { get; set; }

    public DateTime? To { get; set; }

    public bool OnlyActive { get; set; } = true;
}