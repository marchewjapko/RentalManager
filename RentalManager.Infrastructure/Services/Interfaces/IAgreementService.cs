using System.Security.Claims;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Commands.AgreementCommands;
using RentalManager.Infrastructure.DTO;

namespace RentalManager.Infrastructure.Services.Interfaces;

public interface IAgreementService
{
    Task AddAsync(CreateAgreement createAgreement, ClaimsPrincipal user);
    Task<AgreementDto> GetAsync(int id);

    Task<IEnumerable<AgreementDto>> BrowseAllAsync(QueryAgreements queryAgreements);

    Task DeleteAsync(int id);
    Task UpdateAsync(UpdateAgreement updateAgreement, int id);

    Task Deactivate(int id);
}