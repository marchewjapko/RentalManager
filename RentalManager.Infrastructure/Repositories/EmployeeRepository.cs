using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Infrastructure.Exceptions;

namespace RentalManager.Infrastructure.Repositories;

public class EmployeeRepository(AppDbContext appDbContext) : IEmployeeRepository
{
    public async Task<Employee> AddAsync(Employee employee)
    {
        try
        {
            appDbContext.Employees.Add(employee);
            await appDbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Unable to add employee\n" + ex.Message);
        }

        return await Task.FromResult(employee);
    }

    public async Task DeleteAsync(int id)
    {
        var result = await appDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
        {
            throw new EmployeeNotFoundException(id);
        }

        appDbContext.Employees.Remove(result);
        await appDbContext.SaveChangesAsync();
    }

    public async Task<Employee> GetAsync(int id)
    {
        var result = await Task.FromResult(appDbContext.Employees.FirstOrDefault(x => x.Id == id));

        if (result == null)
        {
            throw new EmployeeNotFoundException(id);
        }

        return result;
    }

    public async Task<IEnumerable<Employee>> BrowseAllAsync(string? name = null,
        DateTime? from = null,
        DateTime? to = null)
    {
        var result = appDbContext.Employees.AsQueryable();
        if (name != null)
        {
            result = result.Where(x => x.Name.Contains(name));
        }

        if (from != null)
        {
            result = result.Where(x => x.DateAdded.Date > from.Value.Date);
        }

        if (to != null)
        {
            result = result.Where(x => x.DateAdded.Date < to.Value.Date);
        }

        return await Task.FromResult(result.AsEnumerable());
    }

    public async Task<Employee> UpdateAsync(Employee employee, int id)
    {
        var employeeToUpdate = appDbContext.Employees.FirstOrDefault(x => x.Id == id);

        if (employeeToUpdate == null)
        {
            throw new EmployeeNotFoundException(id);
        }

        employeeToUpdate.Name = employee.Name;
        employeeToUpdate.Surname = employee.Surname;
        employeeToUpdate.Image = employee.Image;
        employeeToUpdate.Gender = employee.Gender;
        await appDbContext.SaveChangesAsync();

        return await Task.FromResult(employeeToUpdate);
    }
}