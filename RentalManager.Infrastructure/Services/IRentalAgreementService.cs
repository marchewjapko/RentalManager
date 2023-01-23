using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Commands;
using RentalManager.Infrastructure.DTO;

namespace RentalManager.Infrastructure.Services
{
    public interface IRentalAgreementService
    {
        Task<RentalAgreementDTO> AddAsync(CreateRentalAgreement createRentalAgreement);
        Task<RentalAgreementDTO> GetAsync(int id);
        Task<IEnumerable<RentalAgreementDTO>> BrowseAllAsync(int? clientId = null, int? rentalEquipmentId = null, bool onlyUnpaid = false, DateTime? from = null, DateTime? to = null);
        Task DeleteAsync(int id);
        Task<RentalAgreementDTO> UpdateAsync(UpdateRentalAgreement updateRentalAgreement, int id);
    }
}
