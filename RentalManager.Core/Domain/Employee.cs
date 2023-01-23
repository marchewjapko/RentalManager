using System.ComponentModel.DataAnnotations;

namespace RentalManager.Core.Domain
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateAdded { get; set; }
    }
}