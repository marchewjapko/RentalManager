using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Infrastructure.Commands.EmployeeCommands;
using RentalManager.Infrastructure.DTO;
using RentalManager.Infrastructure.DTO.ObjectConversions;
using RentalManager.Infrastructure.Services.Interfaces;

namespace RentalManager.Infrastructure.Services.Implementation;

public class EmployeeService(IEmployeeRepository employeeRepository,
    UserManager<Employee> userManager) : IEmployeeService
{
    public async Task<EmployeeDto> AddAsync(CreateEmployee createEmployee)
    {
        var result =
            await userManager.CreateAsync(createEmployee.ToDomain(), createEmployee.Password);

        var x = userManager.UserValidators;

        if (result.Succeeded)
        {
            return createEmployee.ToDomain()
                .ToDto();
        }

        var validationFailure = new List<ValidationFailure>();
        foreach (var error in result.Errors)
            validationFailure.Add(new ValidationFailure(error.Code, error.Description));

        throw new ValidationException(validationFailure);
    }

    public async Task<IEnumerable<EmployeeDto>> BrowseAllAsync(string? name = null,
        DateTime? from = null,
        DateTime? to = null)
    {
        var result = await employeeRepository.BrowseAllAsync(name, from, to);

        return await Task.FromResult(result.Select(x => x.ToDto()));
    }

    public async Task DeleteAsync(int id)
    {
        await employeeRepository.DeleteAsync(id);
    }

    public async Task<EmployeeDto> GetAsync(int id)
    {
        var result = await employeeRepository.GetAsync(id);

        return await Task.FromResult(result.ToDto());
    }

    public async Task<EmployeeDto> UpdateAsync(UpdateEmployee updateEmployee, int id)
    {
        var result = await employeeRepository.UpdateAsync(updateEmployee.ToDomain(), id);

        return await Task.FromResult(result.ToDto());
    }
}