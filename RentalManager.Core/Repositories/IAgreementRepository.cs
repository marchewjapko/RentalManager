using RentalManager.Core.Domain;

namespace RentalManager.Core.Repositories;

public interface IAgreementRepository
{
    Task<Agreement> AddAsync(Agreement agreement);
    Task<Agreement> GetAsync(int id);
    Task DeleteAsync(int id);
    Task<Agreement> UpdateAsync(Agreement agreement, int id);

    Task<IEnumerable<Agreement>> BrowseAllAsync(
        int? clientId = null,
        string? surname = null,
        string? phoneNumber = null,
        string? city = null,
        string? street = null,
        int? equipmentId = null,
        string? equipmentName = null,
        int? employeeId = null,
        bool onlyUnpaid = false,
        DateTime? from = null,
        DateTime? to = null);
}