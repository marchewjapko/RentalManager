using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Infrastructure.Exceptions;

namespace RentalManager.Infrastructure.Repositories;

public class AgreementRepository(AppDbContext appDbContext) : IAgreementRepository
{
    public async Task<Agreement> AddAsync(Agreement agreement)
    {
        try
        {
            var client = appDbContext.Clients.FirstOrDefault(x => x.Id == agreement.ClientId);

            if (client == null)
            {
                throw new ClientNotFoundException(agreement.ClientId);
            }

            var employee =
                appDbContext.Employees.FirstOrDefault(x => x.Id == agreement.EmployeeId);

            if (employee == null)
            {
                throw new EmployeeNotFoundException(agreement.EmployeeId);
            }

            agreement.Client = client;
            agreement.Employee = employee;
            appDbContext.Agreements.Add(agreement);
            await appDbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Unable to add rental agreement\n" + ex.Message);
        }

        return await Task.FromResult(agreement);
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
            .Include(x => x.Employee)
            .Include(x => x.Payments)
            .FirstOrDefault(x => x.Id == id));

        if (result == null)
        {
            throw new AgreementNotFoundException(id);
        }

        return result;
    }

    public async Task<IEnumerable<Agreement>> BrowseAllAsync(
        int? clientId = null,
        string? surname = null,
        string? phoneNumber = null,
        string? city = null,
        string? street = null,
        int? equipmentId = null,
        string? equipmentName = null,
        int? employeeId = null,
        bool onlyUnpaid = false,
        DateTime? from = null,
        DateTime? to = null)
    {
        var result = appDbContext.Agreements.Include(x => x.Equipment)
            .Include(x => x.Client)
            .Include(x => x.Employee)
            .Include(x => x.Payments)
            .AsSingleQuery()
            .AsQueryable();
        if (clientId != null)
        {
            result = result.Where(x => x.ClientId == clientId);
        }

        if (surname != null)
        {
            result = result.Where(x => x.Client.Surname.Contains(surname));
        }

        if (phoneNumber != null)
        {
            result = result.Where(x => x.Client.PhoneNumber.Contains(phoneNumber));
        }

        if (city != null)
        {
            result = result.Where(x => x.Client.City.Contains(city));
        }

        if (street != null)
        {
            result = result.Where(x => x.Client.Street.Contains(street));
        }

        if (equipmentId != null)
        {
            result = result.Where(x => x.Equipment.Any(a => a.Id == equipmentId));
        }

        if (equipmentName != null)
        {
            result = result.Where(x =>
                x.Equipment.Any(a =>
                    a.Name.Contains(equipmentName,
                        StringComparison.CurrentCultureIgnoreCase)));
        }

        if (employeeId != null)
        {
            result = result.Where(x => x.EmployeeId == employeeId);
        }

        if (onlyUnpaid)
        {
            result = result.Where(x =>
                x.Payments.OrderByDescending(payment => payment.DateTo)
                    .First()
                    .DateTo < DateTime.Now.Date);
        }

        if (from != null)
        {
            result = result.Where(x => x.DateAdded.Date > from.Value.Date);
        }

        if (to != null)
        {
            result = result.Where(x => x.DateAdded.Date < to.Value.Date);
        }

        return await Task.FromResult(result.OrderByDescending(x => x.DateAdded)
            .AsEnumerable());
    }

    public async Task<Agreement> UpdateAsync(Agreement agreement, int id)
    {
        var agreementToUpdate = appDbContext.Agreements
            .Include(x => x.Equipment)
            .Include(x => x.Client)
            .Include(x => x.Employee)
            .Include(x => x.Payments)
            .FirstOrDefault(x => x.Id == id);

        if (agreementToUpdate == null)
        {
            throw new AgreementNotFoundException(id);
        }

        agreementToUpdate.IsActive = agreement.IsActive;
        agreementToUpdate.EmployeeId = agreement.EmployeeId;
        agreementToUpdate.Employee = agreement.Employee;
        agreementToUpdate.ClientId = agreement.ClientId;
        agreementToUpdate.Client = agreement.Client;
        agreementToUpdate.Comment = agreement.Comment;
        agreementToUpdate.Deposit = agreement.Deposit;
        agreementToUpdate.Equipment = agreement.Equipment;
        agreementToUpdate.TransportFromPrice = agreement.TransportFromPrice;
        agreementToUpdate.TransportToPrice = agreement.TransportToPrice;
        agreementToUpdate.DateAdded = agreement.DateAdded;
        await appDbContext.SaveChangesAsync();

        return await Task.FromResult(agreementToUpdate);
    }
}