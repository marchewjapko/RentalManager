using System.ComponentModel.DataAnnotations;

namespace RentalManager.Infrastructure.Commands
{
    public class CreateEmployee
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
