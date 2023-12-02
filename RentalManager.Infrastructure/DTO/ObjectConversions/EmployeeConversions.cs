using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Commands.EmployeeCommands;
using RentalManager.Infrastructure.Extensions;

namespace RentalManager.Infrastructure.DTO.ObjectConversions;

public static class EmployeeConversions
{
    public static Employee ToDomain(this CreateEmployee createEmployee)
    {
        return new Employee
        {
            Name = createEmployee.Name,
            Surname = createEmployee.Surname,
            DateAdded = DateTime.Now,
            Gender = (Gender)createEmployee.Gender,
            Image = createEmployee.Image.ToByteArray()
        };
    }

    public static EmployeeDto ToDto(this Employee employee)
    {
        return new EmployeeDto
        {
            Id = employee.Id,
            Name = employee.Name,
            Surname = employee.Surname,
            DateAdded = employee.DateAdded,
            Gender = (GenderDto)employee.Gender,
            Image = employee.Image
        };
    }

    public static EmployeeDto ToDto(this CreateEmployee createEmployee)
    {
        return new EmployeeDto
        {
            Name = createEmployee.Name,
            Surname = createEmployee.Surname,
            Gender = createEmployee.Gender,
            Image = createEmployee.Image.ToByteArray()
        };
    }

    public static EmployeeDto ToDto(this UpdateEmployee updateEmployee)
    {
        return new EmployeeDto
        {
            Name = updateEmployee.Name,
            Surname = updateEmployee.Surname,
            Gender = updateEmployee.Gender,
            Image = updateEmployee.Image.ToByteArray()
        };
    }

    public static Employee ToDomain(this UpdateEmployee updateEmployee)
    {
        var result = new Employee
        {
            Name = updateEmployee.Name,
            Surname = updateEmployee.Surname,
            Gender = (Gender)updateEmployee.Gender,
            Image = updateEmployee.Image.ToByteArray()
        };

        return result;
    }

    public static Employee ToDomain(this EmployeeDto employeeDto)
    {
        var result = new Employee
        {
            Id = employeeDto.Id,
            Name = employeeDto.Name,
            Surname = employeeDto.Surname,
            Gender = (Gender)employeeDto.Gender,
            Image = employeeDto.Image,
            DateAdded = employeeDto.DateAdded,
        };

        return result;
    }
}