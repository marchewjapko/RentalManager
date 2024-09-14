using System.Security.Claims;
using RentalManager.Core.Repositories;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Exceptions;
using RentalManager.Infrastructure.Models.Commands.AgreementCommands;
using RentalManager.Infrastructure.Models.DTO;

namespace RentalManager.Infrastructure.Services.AgreementService;

public class AgreementService(
    IAgreementRepository agreementRepository,
    IEquipmentRepository equipmentRepository,
    IClientRepository clientRepository)
    : IAgreementService
{
    public async Task<AgreementDto> AddAsync(CreateAgreement createAgreement,
        ClaimsPrincipal claimsPrincipal)
    {
        var equipment = (await equipmentRepository.GetAsync(createAgreement.EquipmentIds)).ToList();

        var client = await clientRepository.GetAsync(createAgreement.ClientId);

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

        var agreement = createAgreement.ToDomain();
        agreement.Equipment = equipment.ToList();
        agreement.Client = client;
        //agreement.User = user;
        //foreach (var payment in agreement.Payments) payment.User = user;

        var result = await agreementRepository.AddAsync(agreement);

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
        var equipment = (await equipmentRepository.GetAsync(updateAgreement.EquipmentIds)).ToList();

        if (!equipment.Select(x => x.Id)
                .OrderBy(x => x)
                .SequenceEqual(updateAgreement.EquipmentIds.OrderBy(x => x)))
        {
            var missingIds = updateAgreement.EquipmentIds.Except(equipment.Select(x => x.Id));

            throw new EquipmentNotFoundException(missingIds);
        }

        //agreement.Employee = employee;
        agreement.Equipment = equipment.ToList();

        var result = await agreementRepository.UpdateAsync(agreement, id);

        return result.ToDto();
    }

    public async Task Deactivate(int id)
    {
        await agreementRepository.Deactivate(id);
    }
}