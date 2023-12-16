// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace RentalManager.Global.Requests.GetAgreementDocument;

public class Agreement
{
    public Client Client { get; init; } = null!;

    public IList<Equipment> Equipments { get; init; } = null!;

    public IList<Payment> Payments { get; init; } = null!;

    public int? TransportFrom { get; init; }

    public int TransportTo { get; init; }

    public int Deposit { get; init; }
}