using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Global.Queries;
using RentalManager.Global.Queries.Sorting;
using RentalManager.Infrastructure.ExceptionHandling.Exceptions;
using RentalManager.Infrastructure.Extensions;
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
        var agreementToUpdate = await appDbContext.Agreements
            .Include(x => x.Equipments)
            .Include(x => x.Client)
            .Include(x => x.Payments)
            .FirstOrDefaultAsync(x => x.Id == id);

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
        agreements = agreements.Filter(x => x.ClientId, queryAgreements.ClientId, FilterOperand.Equals);
        agreements = agreements.Filter(x => x.UserId, queryAgreements.UserId, FilterOperand.Equals);
        agreements = agreements.Filter(x => x.Client.LastName, queryAgreements.LastName, FilterOperand.Contains);
        agreements = agreements.Filter(x => x.Client.City, queryAgreements.City, FilterOperand.Contains);
        agreements = agreements.Filter(x => x.Client.Street, queryAgreements.Street, FilterOperand.Contains);
        agreements = agreements.Filter(x => x.DateAdded.Date, queryAgreements.AddedFrom?.Date, FilterOperand.GreaterThanOrEqualTo);
        agreements = agreements.Filter(x => x.DateAdded.Date, queryAgreements.AddedTo?.Date, FilterOperand.LessThanOrEqualTo);

        if (queryAgreements.OnlyActive)
        {
            agreements = agreements.Filter(x => x.IsActive, true, FilterOperand.Equals);
        }

        if (queryAgreements.OnlyUnpaid)
        {
            agreements = agreements.Where(x => x.Payments.Count == 0 || x.Payments.OrderByDescending(payment => payment.DateTo)
                .First()
                .DateTo < DateTime.Now.Date);
        }

        return agreements;
    }

    private static IQueryable<Agreement> SortAgreements(IQueryable<Agreement> agreements,
        QueryAgreements queryAgreements)
    {
        var sortOrder = queryAgreements.Descending ? SortOrder.Desc : SortOrder.Asc;

        agreements = queryAgreements.SortAgreementsBy switch
        {
            SortAgreementsBy.Id => agreements.Sort(x => x.Id, sortOrder),
            SortAgreementsBy.Surname => agreements.Sort(x => x.Client.LastName, sortOrder),
            SortAgreementsBy.DateAdded => agreements.Sort(x => x.DateAdded, sortOrder),
            _ => agreements.Sort(x => x.DateAdded, sortOrder)
        };

        agreements = agreements.Skip(queryAgreements.Position);
        agreements = agreements.Take(queryAgreements.PageSize);

        return agreements;
    }
}