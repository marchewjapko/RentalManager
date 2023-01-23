using RentalManager.Core.Domain;

namespace RentalManager.Core.Repositories
{
    public interface IRentalAgreementRepository
    {
        Task<RentalAgreement> AddAsync(RentalAgreement rentalAgreement);
        Task<RentalAgreement> GetAsync(int id);
        Task DeleteAsync(int id);
        Task<RentalAgreement> UpdateAsync(RentalAgreement rentalAgreement, int id);
        Task<IEnumerable<RentalAgreement>> BrowseAllAsync(int? clientId = null, int? rentalEquipmentId = null, bool onlyUnpaid = false, DateTime? from = null, DateTime? to = null);
    }
}
