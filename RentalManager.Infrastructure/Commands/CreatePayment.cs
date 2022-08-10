namespace RentalManager.Infrastructure.Commands
{
    public class CreatePayment
    {
        public int RentalAgreementId { get; set; }
        public string Method { get; set; }
        public int Amount { get; set; }
    }
}
