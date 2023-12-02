namespace RentalManager.Core.Domain;

public class Payment
{
    public int Id { get; set; }

    public int RentalAgreementId { get; set; }

    public RentalAgreement RentalAgreement { get; set; } = null!;

    public string? Method { get; set; }

    public int Amount { get; set; }

    public DateTime DateFrom { get; set; }

    public DateTime DateTo { get; set; }

    public DateTime DateAdded { get; set; }
}