using System.ComponentModel.DataAnnotations;

namespace RentalManager.Infrastructure.Commands
{
    public class UpdateEmployee
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
