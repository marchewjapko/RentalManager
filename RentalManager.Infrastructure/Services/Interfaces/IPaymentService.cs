using System.Security.Claims;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Commands.PaymentCommands;
using RentalManager.Infrastructure.DTO;

namespace RentalManager.Infrastructure.Services.Interfaces;

public interface IPaymentService
{
    Task AddAsync(CreatePayment createPayment,
        ClaimsPrincipal user);

    Task<PaymentDto> GetAsync(int id);

    Task<IEnumerable<PaymentDto>> BrowseAllAsync(QueryPayment queryPayment);

    Task DeleteAsync(int id);
    Task UpdateAsync(UpdatePayment updatePayment, int id);

    Task Deactivate(int id);
}