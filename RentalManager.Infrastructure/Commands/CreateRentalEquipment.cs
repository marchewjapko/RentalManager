using System.ComponentModel.DataAnnotations;

namespace RentalManager.Infrastructure.Commands
{
    public class CreateRentalEquipment
    {
        [Required]
        public string? Name { get; set; }
        public int MonthlyPrice { get; set; }
    }
}
