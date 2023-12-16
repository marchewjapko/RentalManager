using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Global.Queries;
using RentalManager.Global.Queries.Sorting;
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

        result = FilterAgreements(result, queryAgreements);

        result = SortAgreements(result, queryAgreements);

        return await Task.FromResult(result.AsEnumerable());
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

    private static IQueryable<Agreement> FilterAgreements(IQueryable<Agreement> agreements,
        QueryAgreements queryAgreements)
    {
        if (queryAgreements.ClientId != null)
        {
            agreements = agreements.Where(x => x.ClientId == queryAgreements.ClientId);
        }

        if (queryAgreements.Surname != null)
        {
            agreements = agreements.Where(x => x.Client.Surname.Contains(queryAgreements.Surname));
        }

        if (queryAgreements.City != null)
        {
            agreements = agreements.Where(x => x.Client.City.Contains(queryAgreements.City));
        }

        if (queryAgreements.Street != null)
        {
            agreements = agreements.Where(x => x.Client.Street.Contains(queryAgreements.Street));
        }

        if (queryAgreements.EmployeeId != null)
        {
            agreements = agreements.Where(x => x.EmployeeId == queryAgreements.EmployeeId);
        }

        if (queryAgreements.OnlyUnpaid)
        {
            agreements = agreements.Where(x =>
                x.Payments.OrderByDescending(payment => payment.DateTo)
                    .First()
                    .DateTo < DateTime.Now.Date);
        }

        if (queryAgreements.AddedFrom != null)
        {
            agreements =
                agreements.Where(x => x.DateAdded.Date > queryAgreements.AddedFrom.Value.Date);
        }

        if (queryAgreements.AddedTo != null)
        {
            agreements =
                agreements.Where(x => x.DateAdded.Date < queryAgreements.AddedTo.Value.Date);
        }

        if (queryAgreements.OnlyActive)
        {
            agreements = agreements.Where(x => x.IsActive);
        }

        return agreements;
    }

    private static IQueryable<Agreement> SortAgreements(IQueryable<Agreement> agreements,
        QueryAgreements queryAgreements)
    {
        if (queryAgreements.Descending)
        {
            agreements = queryAgreements.SortAgreementsBy switch
            {
                SortAgreementsBy.Id => agreements.OrderByDescending(x => x.Id),
                SortAgreementsBy.Surname => agreements.OrderByDescending(x => x.Client.Surname),
                SortAgreementsBy.DateAdded => agreements.OrderByDescending(x => x.DateAdded),
                _ => agreements.OrderByDescending(x => x.DateAdded)
            };
        }
        else
        {
            agreements = queryAgreements.SortAgreementsBy switch
            {
                SortAgreementsBy.Id => agreements.OrderBy(x => x.Id),
                SortAgreementsBy.Surname => agreements.OrderBy(x => x.Client.Surname),
                SortAgreementsBy.DateAdded => agreements.OrderBy(x => x.DateAdded),
                _ => agreements.OrderBy(x => x.DateAdded)
            };
        }

        agreements = agreements.Skip(queryAgreements.Position);
        agreements = agreements.Take(queryAgreements.PageSize);

        return agreements;
    }
}