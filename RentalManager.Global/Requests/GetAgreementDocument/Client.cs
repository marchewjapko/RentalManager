namespace RentalManager.Global.Requests.GetAgreementDocument;

public class Client
{
    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? IdCard { get; set; }
}