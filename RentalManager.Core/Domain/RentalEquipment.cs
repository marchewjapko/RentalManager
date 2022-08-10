using System.ComponentModel.DataAnnotations;

namespace RentalManager.Core.Domain
{
    public class RentalEquipment
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public int MonthlyPrice { get; set; }
        public DateTime DateAdded { get; set; }
        public ICollection<RentalAgreement> RentalAgreements { get; set; }
    }
}
