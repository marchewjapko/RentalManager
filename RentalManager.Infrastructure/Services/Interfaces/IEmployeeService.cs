using RentalManager.Infrastructure.Commands.EmployeeCommands;
using RentalManager.Infrastructure.DTO;

namespace RentalManager.Infrastructure.Services.Interfaces;

public interface IEmployeeService
{
    Task<EmployeeDto> AddAsync(CreateEmployee createEmployee);
    Task<EmployeeDto> GetAsync(int id);
    Task DeleteAsync(int id);
    Task<EmployeeDto> UpdateAsync(UpdateEmployee updateEmployee, int id);

    Task<IEnumerable<EmployeeDto>> BrowseAllAsync(string? name = null,
        DateTime? from = null,
        DateTime? to = null);
}