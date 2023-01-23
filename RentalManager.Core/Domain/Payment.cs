using System.ComponentModel.DataAnnotations;

namespace RentalManager.Core.Domain
{
    public class Payment
    {
        public int Id { get; set; }
        public int RentalAgreementId { get; set; }
        public RentalAgreement RentalAgreement { get; set; }
        public string? Method { get; set; }
        public int Amount { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
