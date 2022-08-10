namespace RentalManager.Infrastructure.DTO
{
    public class PaymentDTO
    {
        public int Id { get; set; }
        public int RentalAgreementId { get; set; }
        public string? Method { get; set; }
        public int Amount { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
