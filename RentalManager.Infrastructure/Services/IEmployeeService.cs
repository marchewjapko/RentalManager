using RentalManager.Infrastructure.Commands;
using RentalManager.Infrastructure.DTO;

namespace RentalManager.Infrastructure.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeDTO> AddAsync(CreateEmployee createEmployee);
        Task<EmployeeDTO> GetAsync(int id);
        Task DeleteAsync(int id);
        Task<EmployeeDTO> UpdateAsync(UpdateEmployee updateEmployee, int id);
        Task<IEnumerable<EmployeeDTO>> BrowseAllAsync(string? name = null, DateTime? from = null, DateTime? to = null);
    }
}
