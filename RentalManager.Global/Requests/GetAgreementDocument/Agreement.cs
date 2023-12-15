namespace RentalManager.Global.Requests.GetAgreementDocument;

public class Agreement
{
    public Client Client { get; set; } = null!;

    public IList<Equipment> Equipments { get; set; } = null!;

    public IList<Payment> Payments { get; set; } = null!;

    public int? TransportFrom { get; set; }

    public int TransportTo { get; set; }

    public int Deposit { get; set; }
}