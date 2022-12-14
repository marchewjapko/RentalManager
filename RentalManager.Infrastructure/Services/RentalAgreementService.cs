using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Infrastructure.Commands;
using RentalManager.Infrastructure.DTO;

namespace RentalManager.Infrastructure.Services
{
    public class RentalAgreementService : IRentalAgreementService
    {
        private readonly IRentalAgreementRepository _rentalAgreementRepository;
        private readonly IRentalEquipmentRepository _rentalEquipmentRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public RentalAgreementService(IRentalAgreementRepository rentalAgreementRepository, IRentalEquipmentRepository rentalEquipmentRepository, IClientRepository clientRepository, IEmployeeRepository employeeRepository, IPaymentRepository paymentRepository)
        {
            _rentalAgreementRepository = rentalAgreementRepository;
            _rentalEquipmentRepository = rentalEquipmentRepository;
            _clientRepository = clientRepository;
            _employeeRepository = employeeRepository;
            _paymentRepository = paymentRepository;
        }

        public async Task<RentalAgreementDTO> AddAsync(CreateRentalAgreement createRentalAgreement)
        {
            var rentalEquipment = await _rentalEquipmentRepository.GetAsync(createRentalAgreement.RentalEquipmentIds);
            var agreement = createRentalAgreement.ToDomain();
            agreement.RentalEquipment = rentalEquipment.ToList();
            var result = await _rentalAgreementRepository.AddAsync(agreement);
            result.Payments = (await _paymentRepository.BrowseAllAsync(result.Id, null, null)).ToList();
            return result.ToDTO();
        }

        public async Task<IEnumerable<RentalAgreementDTO>> BrowseAllAsync(int? clientId = null, int? rentalEquipmentId = null, bool onlyUnpaid = false, DateTime? from = null, DateTime? to = null)
        {
            var result = await _rentalAgreementRepository.BrowseAllAsync(clientId, rentalEquipmentId, onlyUnpaid, from, to);
            return await Task.FromResult(result.Select(x => x.ToDTO()));
        }

        public async Task DeleteAsync(int id)
        {
            await _rentalAgreementRepository.DeleteAsync(id);
        }

        public async Task<RentalAgreementDTO> GetAsync(int id)
        {
            var result = await _rentalAgreementRepository.GetAsync(id);
            return await Task.FromResult(result.ToDTO());
        }

        public async Task<RentalAgreementDTO> UpdateAsync(UpdateRentalAgreement updateRentalAgreement, int id)
        {
            var agreement = updateRentalAgreement.ToDomain();
            var rentalEquipment = await _rentalEquipmentRepository.GetAsync(updateRentalAgreement.RentalEquipmentIds);
            var client = await _clientRepository.GetAsync(updateRentalAgreement.ClientId);
            var employee = await _employeeRepository.GetAsync(updateRentalAgreement.EmployeeId);
            agreement.RentalEquipment = rentalEquipment.ToList();
            agreement.Client = client;
            agreement.Employee = employee;
            await _rentalAgreementRepository.UpdateAsync(agreement, id);
            agreement.Payments = (await _paymentRepository.BrowseAllAsync(id, null, null)).ToList();
            return await Task.FromResult(agreement.ToDTO());
        }

        public async Task<RentalAgreementDTO> ExtendValidDateAsync(int rentalAgreementId, DateTime newValidDate)
        {
            var result = await _rentalAgreementRepository.ExtendValidDateAsync(rentalAgreementId, newValidDate);
            return await Task.FromResult(result.ToDTO());
        }
    }
}
