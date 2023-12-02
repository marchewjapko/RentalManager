using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;

namespace RentalManager.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _appDbContext;

    public EmployeeRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Employee> AddAsync(Employee employee)
    {
        try
        {
            _appDbContext.Employees.Add(employee);
            await _appDbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Unable to add employee\n" + ex.Message);
        }

        return await Task.FromResult(employee);
    }

    public async Task DeleteAsync(int id)
    {
        var result = await _appDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

        if (result == null) throw new Exception("Unable to find employee");

        _appDbContext.Employees.Remove(result);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<Employee> GetAsync(int id)
    {
        var result = await Task.FromResult(_appDbContext.Employees.FirstOrDefault(x => x.Id == id));

        if (result == null) throw new Exception("Unable to find employee");

        return result;
    }

    public async Task<IEnumerable<Employee>> BrowseAllAsync(string? name = null,
        DateTime? from = null,
        DateTime? to = null)
    {
        var result = _appDbContext.Employees.AsQueryable();
        if (name != null) result = result.Where(x => x.Name.Contains(name));

        if (from != null) result = result.Where(x => x.DateAdded.Date > from.Value.Date);

        if (to != null) result = result.Where(x => x.DateAdded.Date < to.Value.Date);

        return await Task.FromResult(result.AsEnumerable());
    }

    public async Task<Employee> UpdateAsync(Employee employee, int id)
    {
        var x = _appDbContext.Employees.FirstOrDefault(x => x.Id == id);

        if (x == null) throw new Exception("Unable to update employee");

        x.Name = employee.Name;
        x.Surname = employee.Surname;
        await _appDbContext.SaveChangesAsync();

        return await Task.FromResult(x);
    }
}