using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Global.Queries;
using RentalManager.Global.Queries.Sorting;
using RentalManager.Infrastructure.ExceptionHandling.Exceptions;
using RentalManager.Infrastructure.Repositories.DbContext;

namespace RentalManager.Infrastructure.Repositories;

public class AgreementRepository(AppDbContext appDbContext) : IAgreementRepository
{
    public async Task<Agreement> AddAsync(Agreement agreement)
    {
        var result = appDbContext.Agreements.Add(agreement);

        await appDbContext.SaveChangesAsync();

        return result.Entity;
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
        var result = await appDbContext.Agreements.Include(x => x.Equipments)
            .Include(x => x.Client)
            .Include(x => x.Payments)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
        {
            throw new AgreementNotFoundException(id);
        }

        return result;
    }

    public async Task<IEnumerable<Agreement>> BrowseAllAsync(QueryAgreements queryAgreements)
    {
        var result = appDbContext.Agreements
            .Include(x => x.Equipments)
            .Include(x => x.Client)
            .Include(x => x.Payments)
            .AsSingleQuery()
            .AsQueryable();

        result = FilterAgreements(result, queryAgreements);

        result = SortAgreements(result, queryAgreements);

        return await result.ToListAsync();
    }

    public async Task<Agreement> UpdateAsync(Agreement agreement, int id)
    {
        var agreementToUpdate = appDbContext.Agreements
            .Include(x => x.Equipments)
            .Include(x => x.Client)
            .Include(x => x.Payments)
            .FirstOrDefault(x => x.Id == id);

        if (agreementToUpdate == null)
        {
            throw new AgreementNotFoundException(id);
        }

        agreementToUpdate.Client = agreement.Client;
        agreementToUpdate.UserId = agreement.UserId;
        agreementToUpdate.IsActive = agreement.IsActive;
        agreementToUpdate.Comment = agreement.Comment;
        agreementToUpdate.Deposit = agreement.Deposit;
        agreementToUpdate.Equipments = agreement.Equipments;
        agreementToUpdate.TransportFromPrice = agreement.TransportFromPrice;
        agreementToUpdate.TransportToPrice = agreement.TransportToPrice;
        agreementToUpdate.DateAdded = agreement.DateAdded;
        agreementToUpdate.UpdatedTs = DateTime.Now;
        await appDbContext.SaveChangesAsync();

        return agreementToUpdate;
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
        if (queryAgreements.UserId.HasValue)
        {
            agreements = agreements.Where(x => x.UserId == queryAgreements.UserId.Value);
        }

        if (queryAgreements.ClientId != null)
        {
            agreements = agreements.Where(x => x.ClientId == queryAgreements.ClientId);
        }

        if (queryAgreements.Surname != null)
        {
            agreements = agreements.Where(x => x.Client.LastName.Contains(queryAgreements.Surname));
        }

        if (queryAgreements.City != null)
        {
            agreements = agreements.Where(x => x.Client.City.Contains(queryAgreements.City));
        }

        if (queryAgreements.Street != null)
        {
            agreements = agreements.Where(x => x.Client.Street.Contains(queryAgreements.Street));
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
                SortAgreementsBy.Surname => agreements.OrderByDescending(x => x.Client.LastName),
                SortAgreementsBy.DateAdded => agreements.OrderByDescending(x => x.DateAdded),
                _ => agreements.OrderByDescending(x => x.DateAdded)
            };
        }
        else
        {
            agreements = queryAgreements.SortAgreementsBy switch
            {
                SortAgreementsBy.Id => agreements.OrderBy(x => x.Id),
                SortAgreementsBy.Surname => agreements.OrderBy(x => x.Client.LastName),
                SortAgreementsBy.DateAdded => agreements.OrderBy(x => x.DateAdded),
                _ => agreements.OrderBy(x => x.DateAdded)
            };
        }

        agreements = agreements.Skip(queryAgreements.Position);
        agreements = agreements.Take(queryAgreements.PageSize);

        return agreements;
    }
}