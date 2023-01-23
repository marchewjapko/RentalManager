using System.ComponentModel.DataAnnotations;

namespace RentalManager.Core.Domain
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string IdCard { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
