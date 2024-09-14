using System.Security.Claims;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Models.Commands.AgreementCommands;
using RentalManager.Infrastructure.Models.DTO;

namespace RentalManager.Infrastructure.Services.AgreementService;

public interface IAgreementService
{
    Task<AgreementDto> AddAsync(CreateAgreement createAgreement, ClaimsPrincipal user);

    Task<AgreementDto> GetAsync(int id);

    Task<IEnumerable<AgreementDto>> BrowseAllAsync(QueryAgreements queryAgreements);

    Task DeleteAsync(int id);

    Task<AgreementDto> UpdateAsync(UpdateAgreement updateAgreement, int id);

    Task Deactivate(int id);
}