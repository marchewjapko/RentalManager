namespace RentalManager.Infrastructure.Commands
{
    public class UpdatePayment
    {
        public int RentalAgreementId { get; set; }
        public string Method { get; set; }
        public int Amount { get; set; }
    }
}
