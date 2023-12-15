using RentalManager.Core.Domain;
using RentalManager.Global.Queries;

namespace RentalManager.Core.Repositories;

public interface IAgreementRepository
{
    Task AddAsync(Agreement agreement);

    Task<Agreement> GetAsync(int id);

    Task DeleteAsync(int id);

    Task UpdateAsync(Agreement agreement, int id);

    Task<IEnumerable<Agreement>> BrowseAllAsync(QueryAgreements queryAgreements);

    Task Deactivate(int id);
}