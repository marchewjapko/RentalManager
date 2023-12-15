namespace RentalManager.Global.Requests.GetAgreementDocument;

public class Payment
{
    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public int Value { get; set; }
}