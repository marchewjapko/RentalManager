using RentalManager.Core.Domain;

namespace RentalManager.Core.Repositories;

public interface IEmployeeRepository
{
    Task<Employee> AddAsync(Employee employee);
    Task<Employee> GetAsync(int id);
    Task DeleteAsync(int id);
    Task<Employee> UpdateAsync(Employee employee, int id);

    Task<IEnumerable<Employee>> BrowseAllAsync(string? name = null,
        DateTime? from = null,
        DateTime? to = null);
}