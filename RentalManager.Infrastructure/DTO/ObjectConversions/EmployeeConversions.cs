using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Commands;

namespace RentalManager.Infrastructure.DTO.ObjectConversions;

public static class EmployeeConversions
{
    public static Employee ToDomain(this CreateEmployee createEmployee)
    {
        return new Employee
        {
            Name = createEmployee.Name,
            Surname = createEmployee.Surname,
            DateAdded = DateTime.Now
        };
    }

    public static EmployeeDto ToDto(this Employee employee)
    {
        return new EmployeeDto
        {
            Id = employee.Id,
            Name = employee.Name,
            Surname = employee.Surname,
            DateAdded = employee.DateAdded
        };
    }

    public static EmployeeDto ToDto(this CreateEmployee createEmployee)
    {
        return new EmployeeDto
        {
            Name = createEmployee.Name,
            Surname = createEmployee.Surname
        };
    }

    public static EmployeeDto ToDto(this UpdateEmployee updateEmployee)
    {
        return new EmployeeDto
        {
            Name = updateEmployee.Name,
            Surname = updateEmployee.Surname
        };
    }

    public static Employee ToDomain(this UpdateEmployee updateEmployee)
    {
        var result = new Employee
        {
            Name = updateEmployee.Name,
            Surname = updateEmployee.Surname
        };
        return result;
    }
}