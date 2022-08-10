using System.ComponentModel.DataAnnotations;

namespace RentalManager.Core.Domain
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Surname { get; set; }
        public DateTime DateAdded { get; set; }
    }
}