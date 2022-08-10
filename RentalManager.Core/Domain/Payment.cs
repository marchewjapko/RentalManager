using System.ComponentModel.DataAnnotations;

namespace RentalManager.Core.Domain
{
    public class Payment
    {
        public int Id { get; set; }
        [Required]
        public int RentalAgreementId { get; set; }
        public RentalAgreement? RentalAgreement { get; set; }
        public string? Method { get; set; }
        [Required]
        public int Amount { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
