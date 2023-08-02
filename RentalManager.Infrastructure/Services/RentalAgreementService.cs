using RentalManager.Core.Repositories;
using RentalManager.Infrastructure.Commands.RentalAgreementCommands;
using RentalManager.Infrastructure.DTO;
using RentalManager.Infrastructure.DTO.ObjectConversions;

namespace RentalManager.Infrastructure.Services;

public class RentalAgreementService : IRentalAgreementService
{
    private readonly IClientRepository _clientRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IRentalAgreementRepository _rentalAgreementRepository;
    private readonly IRentalEquipmentRepository _rentalEquipmentRepository;

    public RentalAgreementService(IRentalAgreementRepository rentalAgreementRepository,
        IRentalEquipmentRepository rentalEquipmentRepository, IClientRepository clientRepository,
        IEmployeeRepository employeeRepository, IPaymentRepository paymentRepository)
    {
        _rentalAgreementRepository = rentalAgreementRepository;
        _rentalEquipmentRepository = rentalEquipmentRepository;
        _clientRepository = clientRepository;
        _employeeRepository = employeeRepository;
        _paymentRepository = paymentRepository;
    }

    public async Task<RentalAgreementDto> AddAsync(CreateRentalAgreement createRentalAgreement)
    {
        var rentalEquipment = await _rentalEquipmentRepository.GetAsync(createRentalAgreement.RentalEquipmentIds);
        var agreement = createRentalAgreement.ToDomain();
        agreement.RentalEquipment = rentalEquipment.ToList();
        var result = await _rentalAgreementRepository.AddAsync(agreement);
        result.Payments = (await _paymentRepository.BrowseAllAsync(result.Id)).ToList();
        return result.ToDto();
    }

    public async Task<IEnumerable<RentalAgreementDto>> BrowseAllAsync(
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
        DateTime? to = null)
    {
        var result = await _rentalAgreementRepository.BrowseAllAsync(
            clientId,
            surname,
            phoneNumber,
            city,
            street,
            rentalEquipmentId,
            rentalEquipmentName,
            employeeId,
            onlyUnpaid,
            from,
            to);
        return await Task.FromResult(result.Select(x => x.ToDto()));
    }

    public async Task DeleteAsync(int id)
    {
        await _rentalAgreementRepository.DeleteAsync(id);
    }

    public async Task<RentalAgreementDto> GetAsync(int id)
    {
        var result = await _rentalAgreementRepository.GetAsync(id);
        return await Task.FromResult(result.ToDto());
    }

    public async Task<RentalAgreementDto> UpdateAsync(UpdateRentalAgreement updateRentalAgreement, int id)
    {
        var agreement = updateRentalAgreement.ToDomain();
        var rentalEquipment = await _rentalEquipmentRepository.GetAsync(updateRentalAgreement.RentalEquipmentIds);
        var client = await _clientRepository.GetAsync(updateRentalAgreement.ClientId);
        var employee = await _employeeRepository.GetAsync(updateRentalAgreement.EmployeeId);
        agreement.RentalEquipment = rentalEquipment.ToList();
        agreement.Client = client;
        agreement.Employee = employee;
        await _rentalAgreementRepository.UpdateAsync(agreement, id);
        agreement.Payments = (await _paymentRepository.BrowseAllAsync(id)).ToList();
        return await Task.FromResult(agreement.ToDto());
    }
}