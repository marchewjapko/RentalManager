using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalManager.Core.Domain
{
    public class RentalAgreement : IEquatable<RentalAgreement>
    {
        public int Id { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int ClientId { get; set; }
        public Client? Client { get; set; }
        public string? Comment { get; set; }
        [Required]
        public int Deposit { get; set; }
        [Required]
        public int TransportFrom { get; set; }
        public int? TransportTo { get; set; }
        public DateTime ValidUntil { get; set; }
        public DateTime DateAdded { get; set; }

        public ICollection<RentalEquipment> RentalEquipment { get; set; }
        public ICollection<Payment> Payments { get; set; }

        public bool Equals(RentalAgreement other)
        {
            if (other is null)
                return false;

            return Id == other.Id;
        }
        public override bool Equals(object obj) => Equals(obj as RentalAgreement);
        public override int GetHashCode() => (Id).GetHashCode();
    }
}