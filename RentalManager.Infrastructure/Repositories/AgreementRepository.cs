﻿using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Infrastructure.Exceptions;

namespace RentalManager.Infrastructure.Repositories;

public class AgreementRepository : IAgreementRepository
{
    private readonly AppDbContext _appDbContext;

    public AgreementRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Agreement> AddAsync(Agreement agreement)
    {
        try
        {
            var client = _appDbContext.Clients.FirstOrDefault(x => x.Id == agreement.ClientId);

            if (client == null)
            {
                throw new ClientNotFoundException(agreement.ClientId);
            }

            var employee =
                _appDbContext.Employees.FirstOrDefault(x => x.Id == agreement.EmployeeId);

            if (employee == null)
            {
                throw new EmployeeNotFoundException(agreement.EmployeeId);
            }

            agreement.Client = client;
            agreement.Employee = employee;
            _appDbContext.Agreements.Add(agreement);
            await _appDbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Unable to add rental agreement\n" + ex.Message);
        }

        return await Task.FromResult(agreement);
    }

    public async Task DeleteAsync(int id)
    {
        var result = await _appDbContext.Agreements.FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
        {
            throw new AgreementNotFoundException(id);
        }

        _appDbContext.Agreements.Remove(result);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<Agreement> GetAsync(int id)
    {
        var result = await Task.FromResult(_appDbContext.Agreements.Include(x => x.Equipment)
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
        var result = _appDbContext.Agreements.Include(x => x.Equipment)
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
        var x = _appDbContext.Agreements
            .Include(x => x.Equipment)
            .Include(x => x.Client)
            .Include(x => x.Employee)
            .Include(x => x.Payments)
            .FirstOrDefault(x => x.Id == id);

        if (x == null)
        {
            throw new AgreementNotFoundException(id);
        }

        x.IsActive = agreement.IsActive;
        x.EmployeeId = agreement.EmployeeId;
        x.Employee = agreement.Employee;
        x.ClientId = agreement.ClientId;
        x.Client = agreement.Client;
        x.Comment = agreement.Comment;
        x.Deposit = agreement.Deposit;
        x.Equipment = agreement.Equipment;
        x.TransportFromPrice = agreement.TransportFromPrice;
        x.TransportToPrice = agreement.TransportToPrice;
        x.DateAdded = agreement.DateAdded;
        await _appDbContext.SaveChangesAsync();

        return await Task.FromResult(x);
    }
}