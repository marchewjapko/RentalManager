using RentalManager.Infrastructure.Commands.RentalAgreementCommands;
using RentalManager.Infrastructure.DTO;

namespace RentalManager.Infrastructure.Services;

public interface IRentalAgreementService
{
    Task<RentalAgreementDto> AddAsync(CreateRentalAgreement createRentalAgreement);
    Task<RentalAgreementDto> GetAsync(int id);

    Task<IEnumerable<RentalAgreementDto>> BrowseAllAsync(
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
    Task<RentalAgreementDto> UpdateAsync(UpdateRentalAgreement updateRentalAgreement, int id);
}