using System.ComponentModel.DataAnnotations;

namespace RentalManager.Infrastructure.Commands
{
    public class CreateClient
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Surname { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? IdCard { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? StreetNumber { get; set; }
    }
}
