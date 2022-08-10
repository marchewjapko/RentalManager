using RentalManager.Core.Repositories;
using RentalManager.Infrastructure.Commands;
using RentalManager.Infrastructure.DTO;

namespace RentalManager.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeDTO> AddAsync(CreateEmployee createEmployee)
        {
            var result = await _employeeRepository.AddAsync(createEmployee.ToDomain());
            return result.ToDTO();
        }

        public async Task<IEnumerable<EmployeeDTO>> BrowseAllAsync(string? name = null, DateTime? from = null, DateTime? to = null)
        {
            var result = await _employeeRepository.BrowseAllAsync(name, from, to);
            return await Task.FromResult(result.Select(x => x.ToDTO()));
        }

        public async Task DeleteAsync(int id)
        {
            await _employeeRepository.DeleteAsync(id);
        }

        public async Task<EmployeeDTO> GetAsync(int id)
        {
            var result = await _employeeRepository.GetAsync(id);
            return await Task.FromResult(result.ToDTO());
        }

        public async Task<EmployeeDTO> UpdateAsync(UpdateEmployee updateEmployee, int id)
        {
            var result = await _employeeRepository.UpdateAsync(updateEmployee.ToDomain(), id);
            return await Task.FromResult(result.ToDTO());
        }
    }
}
