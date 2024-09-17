using System.Security.Claims;
using AutoMapper;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Extensions;
using RentalManager.Infrastructure.Models.Commands.AgreementCommands;
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

        agreement.Client = await clientRepository.GetAsync(createAgreement.ClientId);
        agreement.Equipments = await equipmentRepository.GetAsync(createAgreement.EquipmentsIds);

        var newAgreement = await agreementRepository.AddAsync(agreement);

        return mapper.Map<AgreementDto>(newAgreement, options => {
            options.AfterMap((o, dto) => dto.User = user);
        });
    }

    public async Task<IEnumerable<AgreementDto>> BrowseAllAsync(QueryAgreements queryAgreements)
    {
        var agreements = await agreementRepository.BrowseAllAsync(queryAgreements);
        var result = mapper.Map<IEnumerable<AgreementDto>>(agreements).ToList();

        foreach (var agreement in result)
            agreement.User = await userService.GetAsync(agreement.User.Id);
        
        return result;
    }

    public Task DeleteAsync(int id)
    {
        return agreementRepository.DeleteAsync(id);
    }

    public async Task<AgreementDto> GetAsync(int id)
    {
        var agreement = await agreementRepository.GetAsync(id);
        var user = await userService.GetAsync(agreement.UserId);
        
        return mapper.Map<AgreementDto>(agreement, options => {
            options.AfterMap((o, dto) => dto.User = user);
        });
    }

    public async Task<AgreementDto> UpdateAsync(UpdateAgreement updateAgreement,
        int id,
        ClaimsPrincipal principal)
    {
        var user = await userService.GetAsync(updateAgreement.UserId);
        var agreement = mapper.Map<Agreement>(updateAgreement);

        FillCreatedBy(agreement, principal.GetId());

        agreement.Client = await clientRepository.GetAsync(updateAgreement.ClientId);
        agreement.Equipments = await equipmentRepository.GetAsync(updateAgreement.EquipmentsIds);

        var updatedAgreement = await agreementRepository.UpdateAsync(agreement, id);
        
        return mapper.Map<AgreementDto>(updatedAgreement, options => {
            options.AfterMap((o, dto) => dto.User = user);
        });
    }

    public Task Deactivate(int id)
    {
        return agreementRepository.Deactivate(id);
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