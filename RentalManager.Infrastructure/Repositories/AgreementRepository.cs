using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Exceptions;
using RentalManager.Infrastructure.Repositories.DbContext;

namespace RentalManager.Infrastructure.Repositories;

public class AgreementRepository(AppDbContext appDbContext) : IAgreementRepository
{
    public async Task AddAsync(Agreement agreement)
    {
        appDbContext.Agreements.Add(agreement);
        await appDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var result = await appDbContext.Agreements.FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
        {
            throw new AgreementNotFoundException(id);
        }

        appDbContext.Agreements.Remove(result);
        await appDbContext.SaveChangesAsync();
    }

    public async Task<Agreement> GetAsync(int id)
    {
        var result = await Task.FromResult(appDbContext.Agreements.Include(x => x.Equipment)
            .Include(x => x.Client)
            .Include(x => x.User)
            .Include(x => x.Employee)
            .Include(x => x.Payments)
            .FirstOrDefault(x => x.Id == id));

        if (result == null)
        {
            throw new AgreementNotFoundException(id);
        }

        return result;
    }

    public async Task<IEnumerable<Agreement>> BrowseAllAsync(QueryAgreements queryAgreements)
    {
        var result = appDbContext.Agreements
            .Include(x => x.Equipment)
            .Include(x => x.Client)
            .Include(x => x.User)
            .Include(x => x.Employee)
            .Include(x => x.Payments)
            .AsSingleQuery()
            .AsQueryable();

        if (queryAgreements.ClientId != null)
        {
            result = result.Where(x => x.ClientId == queryAgreements.ClientId);
        }

        if (queryAgreements.Surname != null)
        {
            result = result.Where(x => x.Client.Surname.Contains(queryAgreements.Surname));
        }

        if (queryAgreements.PhoneNumber != null)
        {
            result = result.Where(x => x.Client.PhoneNumber.Contains(queryAgreements.PhoneNumber));
        }

        if (queryAgreements.City != null)
        {
            result = result.Where(x => x.Client.City.Contains(queryAgreements.City));
        }

        if (queryAgreements.Street != null)
        {
            result = result.Where(x => x.Client.Street.Contains(queryAgreements.Street));
        }

        if (queryAgreements.EquipmentId != null)
        {
            result = result.Where(x => x.Equipment.Any(a => a.Id == queryAgreements.EquipmentId));
        }

        if (queryAgreements.EquipmentName != null)
        {
            result = result.Where(x =>
                x.Equipment.Any(a =>
                    a.Name.Contains(queryAgreements.EquipmentName,
                        StringComparison.CurrentCultureIgnoreCase)));
        }

        if (queryAgreements.EmployeeId != null)
        {
            result = result.Where(x => x.EmployeeId == queryAgreements.EmployeeId);
        }

        if (queryAgreements.OnlyUnpaid)
        {
            result = result.Where(x =>
                x.Payments.OrderByDescending(payment => payment.DateTo)
                    .First()
                    .DateTo < DateTime.Now.Date);
        }

        if (queryAgreements.From != null)
        {
            result = result.Where(x => x.DateAdded.Date > queryAgreements.From.Value.Date);
        }

        if (queryAgreements.To != null)
        {
            result = result.Where(x => x.DateAdded.Date < queryAgreements.To.Value.Date);
        }

        if (queryAgreements.OnlyActive)
        {
            result = result.Where(x => x.IsActive);
        }

        return await Task.FromResult(result.OrderByDescending(x => x.DateAdded)
            .AsEnumerable());
    }

    public async Task UpdateAsync(Agreement agreement, int id)
    {
        var agreementToUpdate = appDbContext.Agreements
            .Include(x => x.Equipment)
            .Include(x => x.Client)
            .Include(x => x.User)
            .Include(x => x.Payments)
            .FirstOrDefault(x => x.Id == id);

        if (agreementToUpdate == null)
        {
            throw new AgreementNotFoundException(id);
        }

        agreementToUpdate.IsActive = agreement.IsActive;
        agreementToUpdate.EmployeeId = agreement.EmployeeId;
        agreementToUpdate.Employee = agreement.Employee;
        agreementToUpdate.Comment = agreement.Comment;
        agreementToUpdate.Deposit = agreement.Deposit;
        agreementToUpdate.Equipment = agreement.Equipment;
        agreementToUpdate.TransportFromPrice = agreement.TransportFromPrice;
        agreementToUpdate.TransportToPrice = agreement.TransportToPrice;
        agreementToUpdate.DateAdded = agreement.DateAdded;
        agreementToUpdate.UpdatedTs = DateTime.Now;
        await appDbContext.SaveChangesAsync();
    }

    public async Task Deactivate(int id)
    {
        var result = await appDbContext.Agreements.FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
        {
            throw new AgreementNotFoundException(id);
        }

        result.IsActive = false;
        await appDbContext.SaveChangesAsync();
    }
}