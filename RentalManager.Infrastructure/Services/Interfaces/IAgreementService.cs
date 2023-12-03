using RentalManager.Infrastructure.Commands.AgreementCommands;
using RentalManager.Infrastructure.DTO;

namespace RentalManager.Infrastructure.Services;

public interface IAgreementService
{
    Task<AgreementDto> AddAsync(CreateAgreement createAgreement);
    Task<AgreementDto> GetAsync(int id);

    Task<IEnumerable<AgreementDto>> BrowseAllAsync(
        int? clientId = null,
        string? surname = null,
        string? phoneNumber = null,
        string? city = null,
        string? street = null,
        int? rentalEquipmentId = null,
        string? rentalEquipmentName = null,
        int? employeeId = null,
        bool onlyUnpaid = false,
        DateTime? from = null,
        DateTime? to = null);

    Task DeleteAsync(int id);
    Task<AgreementDto> UpdateAsync(UpdateAgreement updateAgreement, int id);
}