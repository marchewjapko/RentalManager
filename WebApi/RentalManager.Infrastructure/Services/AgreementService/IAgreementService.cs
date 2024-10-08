﻿using System.Security.Claims;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Models.Commands.AgreementCommands;
using RentalManager.Infrastructure.Models.DTO;

namespace RentalManager.Infrastructure.Services.AgreementService;

public interface IAgreementService
{
    Task<AgreementDto> AddAsync(CreateAgreementCommand createAgreement, ClaimsPrincipal principal);

    Task<AgreementDto> GetAsync(int id);

    Task<IEnumerable<AgreementDto>> BrowseAllAsync(QueryAgreements queryAgreements);

    Task DeleteAsync(int id);

    Task<AgreementDto> UpdateAsync(UpdateAgreementCommand updateAgreement,
        int id,
        ClaimsPrincipal principal);

    Task Deactivate(int id);
}