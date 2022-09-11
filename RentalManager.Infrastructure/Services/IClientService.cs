using RentalManager.Infrastructure.Commands;
using RentalManager.Infrastructure.DTO;

namespace RentalManager.Infrastructure.Services
{
    public interface IClientService
    {
        Task<ClientDTO> AddAsync(CreateClient createClient);
        Task<ClientDTO> GetAsync(int id);
        Task DeleteAsync(int id);
        Task<ClientDTO> UpdateAsync(UpdateClient updateClient, int id);
        Task<IEnumerable<ClientDTO>> BrowseAllAsync(string? name = null, string? surname = null, string? phoneNumber = null, string? email = null, string? idCard = null, string? city = null, string? street = null, DateTime? from = null, DateTime? to = null);
    }
}
