using System.ComponentModel.DataAnnotations;

namespace RentalManager.Infrastructure.Commands
{
    public class CreateEmployee
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Surname { get; set; }
    }
}
