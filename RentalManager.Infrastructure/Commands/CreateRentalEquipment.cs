using System.ComponentModel.DataAnnotations;

namespace RentalManager.Infrastructure.Commands
{
    public class CreateRentalEquipment
    {
        public string Name { get; set; }
        public int Price { get; set; }
    }
}
