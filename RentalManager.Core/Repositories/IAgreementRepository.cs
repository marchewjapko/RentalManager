using RentalManager.Core.Domain;
using RentalManager.Global.Queries;

namespace RentalManager.Core.Repositories;

public interface IAgreementRepository
{
    Task<Agreement> AddAsync(Agreement agreement);

    Task<Agreement> GetAsync(int id);

    Task DeleteAsync(int id);

    Task<Agreement> UpdateAsync(Agreement agreement, int id);

    Task<IEnumerable<Agreement>> BrowseAllAsync(QueryAgreements queryAgreements);

    Task Deactivate(int id);
}