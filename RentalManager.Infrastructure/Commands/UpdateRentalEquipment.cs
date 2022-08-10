using System.ComponentModel.DataAnnotations;

namespace RentalManager.Infrastructure.Commands
{
    public class UpdateRentalEquipment
    {
        [Required]
        public string? Name { get; set; }
        public int MonthlyPrice { get; set; }
    }
}
