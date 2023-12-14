using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Commands.AgreementCommands;
using RentalManager.Infrastructure.DTO;
using RentalManager.Infrastructure.DTO.ObjectConversions;
using RentalManager.Infrastructure.Exceptions;
using RentalManager.Infrastructure.Services.Interfaces;

namespace RentalManager.Infrastructure.Services.Implementation;

public class AgreementService(IAgreementRepository agreementRepository,
        IEquipmentRepository equipmentRepository,
        IClientRepository clientRepository,
        IUserRepository userRepository,
        IPaymentRepository paymentRepository,
        UserManager<User> userManager)
    : IAgreementService
{
    public async Task<AgreementDto> AddAsync(CreateAgreement createAgreement,
        ClaimsPrincipal claimsPrincipal)
    {
        var equipment = await equipmentRepository.GetAsync(createAgreement.EquipmentIds);
        var client = await clientRepository.GetAsync(createAgreement.ClientId);
        var employee = await userRepository.GetAsync(createAgreement.EmployeeId);
        var user = (await userManager.GetUserAsync(claimsPrincipal))!;

        if (!equipment.Select(x => x.Id)
                .OrderBy(x => x)
                .SequenceEqual(createAgreement.EquipmentIds.OrderBy(x => x)))
        {
            var missingIds = createAgreement.EquipmentIds.Except(equipment.Select(x => x.Id));

            throw new EquipmentNotFoundException(missingIds);
        }

        if (client is null)
        {
            throw new ClientNotFoundException(createAgreement.ClientId);
        }

        if (employee is null)
        {
            throw new UserNotFoundException(createAgreement.EmployeeId);
        }

        var agreement = createAgreement.ToDomain();
        agreement.Equipment = equipment.ToList();
        agreement.Client = client;
        agreement.User = user;
        foreach (var payment in agreement.Payments) payment.User = user;

        var result = await agreementRepository.AddAsync(agreement);
        result.Payments = (await paymentRepository.BrowseAllAsync(result.Id)).ToList();

        return result.ToDto();
    }

    public async Task<IEnumerable<AgreementDto>> BrowseAllAsync(QueryAgreements queryAgreements)
    {
        var result = await agreementRepository.BrowseAllAsync(queryAgreements);

        return await Task.FromResult(result.Select(x => x.ToDto()));
    }

    public async Task DeleteAsync(int id)
    {
        await agreementRepository.DeleteAsync(id);
    }

    public async Task<AgreementDto> GetAsync(int id)
    {
        var result = await agreementRepository.GetAsync(id);

        return await Task.FromResult(result.ToDto());
    }

    public async Task<AgreementDto> UpdateAsync(UpdateAgreement updateAgreement, int id)
    {
        var agreement = updateAgreement.ToDomain();
        var rentalEquipment = await equipmentRepository.GetAsync(updateAgreement.EquipmentIds);
        var client = await clientRepository.GetAsync(updateAgreement.ClientId);
        var user = await userRepository.GetAsync(updateAgreement.EmployeeId);
        agreement.Equipment = rentalEquipment.ToList();
        agreement.Client = client;
        agreement.User = user;
        await agreementRepository.UpdateAsync(agreement, id);
        agreement.Payments = (await paymentRepository.BrowseAllAsync(id)).ToList();

        return await Task.FromResult(agreement.ToDto());
    }

    public async Task Deactivate(int id)
    {
        await agreementRepository.Deactivate(id);
    }
}