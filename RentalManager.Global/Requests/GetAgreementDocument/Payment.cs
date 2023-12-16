// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace RentalManager.Global.Requests.GetAgreementDocument;

public class Payment
{
    public DateTime Start { get; init; }

    public DateTime End { get; init; }

    public int Value { get; init; }
}