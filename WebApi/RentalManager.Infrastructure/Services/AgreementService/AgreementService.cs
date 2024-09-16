using System.Security.Claims;
using AutoMapper;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.ExceptionHandling.Exceptions;
using RentalManager.Infrastructure.Extensions;
using RentalManager.Infrastructure.Models.Commands.AgreementCommands;
using RentalManager.Infrastructure.Models.Commands.ClientCommands;
using RentalManager.Infrastructure.Models.Commands.EquipmentCommands;
using RentalManager.Infrastructure.Models.DTO;
using RentalManager.Infrastructure.Services.UserService;

namespace RentalManager.Infrastructure.Services.AgreementService;

public class AgreementService(
    IAgreementRepository agreementRepository,
    IUserService userService,
    IClientRepository clientRepository,
    IEquipmentRepository equipmentRepository,
    IMapper mapper)
    : IAgreementService
{
    public async Task<AgreementDto> AddAsync(CreateAgreement createAgreement,
        ClaimsPrincipal principal)
    {
        var user = await userService.GetAsync(createAgreement.UserId);
        var agreement = mapper.Map<Agreement>(createAgreement);

        FillCreatedBy(agreement, principal.GetId());
        FillCreatedBy(agreement.Payments, principal.GetId());

        await FillClient(createAgreement.Client, agreement);
        await FillEquipments(createAgreement.Equipments, agreement);

        var newAgreement = await agreementRepository.AddAsync(agreement);
        var result = mapper.Map<AgreementDto>(newAgreement);
        result.User = user;

        return result;
    }

    public async Task<IEnumerable<AgreementDto>> BrowseAllAsync(QueryAgreements queryAgreements)
    {
        var agreements = await agreementRepository.BrowseAllAsync(queryAgreements);
        var result = mapper.Map<IEnumerable<AgreementDto>>(agreements);

        foreach (var agreement in result)
            agreement.User = await userService.GetAsync(agreement.User.Id);

        return mapper.Map<IEnumerable<AgreementDto>>(result);
    }

    public async Task DeleteAsync(int id)
    {
        await agreementRepository.DeleteAsync(id);
    }

    public async Task<AgreementDto> GetAsync(int id)
    {
        var agreement = await agreementRepository.GetAsync(id);
        var result = mapper.Map<AgreementDto>(agreement);
        result.User = await userService.GetAsync(result.User.Id);

        return result;
    }

    public async Task<AgreementDto> UpdateAsync(UpdateAgreement updateAgreement,
        int id,
        ClaimsPrincipal principal)
    {
        var user = await userService.GetAsync(updateAgreement.UserId);
        var agreement = mapper.Map<Agreement>(updateAgreement);

        FillCreatedBy(agreement, principal.GetId());

        await FillClient(updateAgreement.Client, agreement);
        await FillEquipments(updateAgreement.Equipments, agreement);

        var newAgreement = await agreementRepository.UpdateAsync(agreement, id);
        var result = mapper.Map<AgreementDto>(newAgreement);
        result.User = user;

        return result;
    }

    public async Task Deactivate(int id)
    {
        await agreementRepository.Deactivate(id);
    }

    public async Task FillEquipments(List<CreateOrGetEquipment> equipments, Agreement agreement)
    {
        var equipmentIds = equipments.Where(x => x.Id.HasValue)
            .Select(x => x.Id!.Value)
            .ToList();

        var existingEquipments = await equipmentRepository.GetAsync(equipmentIds);

        existingEquipments = existingEquipments.ToList();

        foreach (var equipment in equipments)
            if (equipment.Id.HasValue)
            {
                var existingEquipment =
                    existingEquipments.FirstOrDefault(x => x.Id == equipment.Id.Value);

                if (existingEquipment == null)
                {
                    throw new EquipmentNotFoundException(equipment.Id.Value);
                }

                agreement.Equipments.Add(existingEquipment);
            }
            else
            {
                var newEquipment = mapper.Map<Equipment>(equipment);

                newEquipment.CreatedBy = agreement.CreatedBy;

                agreement.Equipments.Add(newEquipment);
            }
    }

    public async Task FillClient(CreateOrGetClient client, Agreement agreement)
    {
        if (client.Id.HasValue)
        {
            var existingClient = await clientRepository.GetAsync(client.Id.Value);

            agreement.ClientId = existingClient.Id;
            agreement.Client = existingClient;
        }
    }

    private static void FillCreatedBy(DomainBase domain, int id)
    {
        domain.CreatedBy = id;
    }

    private static void FillCreatedBy(IEnumerable<DomainBase> domains, int id)
    {
        foreach (var domain in domains) domain.CreatedBy = id;
    }
}