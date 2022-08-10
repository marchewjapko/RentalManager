using RentalManager.Core.Domain;

namespace RentalManager.Infrastructure.DTO
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? IdCard { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
