using RentalManager.Core.Repositories;
using RentalManager.Infrastructure.Commands.EmployeeCommands;
using RentalManager.Infrastructure.DTO;
using RentalManager.Infrastructure.DTO.ObjectConversions;

namespace RentalManager.Infrastructure.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<EmployeeDto> AddAsync(CreateEmployee createEmployee)
    {
        var result = await _employeeRepository.AddAsync(createEmployee.ToDomain());

        return result.ToDto();
    }

    public async Task<IEnumerable<EmployeeDto>> BrowseAllAsync(string? name = null,
        DateTime? from = null,
        DateTime? to = null)
    {
        var result = await _employeeRepository.BrowseAllAsync(name, from, to);

        return await Task.FromResult(result.Select(x => x.ToDto()));
    }

    public async Task DeleteAsync(int id)
    {
        await _employeeRepository.DeleteAsync(id);
    }

    public async Task<EmployeeDto> GetAsync(int id)
    {
        var result = await _employeeRepository.GetAsync(id);

        return await Task.FromResult(result.ToDto());
    }

    public async Task<EmployeeDto> UpdateAsync(UpdateEmployee updateEmployee, int id)
    {
        var result = await _employeeRepository.UpdateAsync(updateEmployee.ToDomain(), id);

        return await Task.FromResult(result.ToDto());
    }
}