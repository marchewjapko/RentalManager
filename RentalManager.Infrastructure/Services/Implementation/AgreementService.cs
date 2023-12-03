using RentalManager.Core.Repositories;
using RentalManager.Infrastructure.Commands.AgreementCommands;
using RentalManager.Infrastructure.DTO;
using RentalManager.Infrastructure.DTO.ObjectConversions;
using RentalManager.Infrastructure.Services.Interfaces;

namespace RentalManager.Infrastructure.Services.Implementation;

public class AgreementService : IAgreementService
{
    private readonly IAgreementRepository _agreementRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly IPaymentRepository _paymentRepository;

    public AgreementService(IAgreementRepository agreementRepository,
        IEquipmentRepository equipmentRepository,
        IClientRepository clientRepository,
        IEmployeeRepository employeeRepository,
        IPaymentRepository paymentRepository)
    {
        _agreementRepository = agreementRepository;
        _equipmentRepository = equipmentRepository;
        _clientRepository = clientRepository;
        _employeeRepository = employeeRepository;
        _paymentRepository = paymentRepository;
    }

    public async Task<AgreementDto> AddAsync(CreateAgreement createAgreement)
    {
        var rentalEquipment = await _equipmentRepository.GetAsync(createAgreement.EquipmentIds);
        var agreement = createAgreement.ToDomain();
        agreement.Equipment = rentalEquipment.ToList();
        var result = await _agreementRepository.AddAsync(agreement);
        result.Payments = (await _paymentRepository.BrowseAllAsync(result.Id)).ToList();

        return result.ToDto();
    }

    public async Task<IEnumerable<AgreementDto>> BrowseAllAsync(
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
        var result = await _agreementRepository.BrowseAllAsync(
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
        await _agreementRepository.DeleteAsync(id);
    }

    public async Task<AgreementDto> GetAsync(int id)
    {
        var result = await _agreementRepository.GetAsync(id);

        return await Task.FromResult(result.ToDto());
    }

    public async Task<AgreementDto> UpdateAsync(UpdateAgreement updateAgreement, int id)
    {
        var agreement = updateAgreement.ToDomain();
        var rentalEquipment = await _equipmentRepository.GetAsync(updateAgreement.EquipmentIds);
        var client = await _clientRepository.GetAsync(updateAgreement.ClientId);
        var employee = await _employeeRepository.GetAsync(updateAgreement.EmployeeId);
        agreement.Equipment = rentalEquipment.ToList();
        agreement.Client = client;
        agreement.Employee = employee;
        await _agreementRepository.UpdateAsync(agreement, id);
        agreement.Payments = (await _paymentRepository.BrowseAllAsync(id)).ToList();

        return await Task.FromResult(agreement.ToDto());
    }
}