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
            UserName = createEmployee.UserName,
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
            UserName = employee.UserName,
            Surname = employee.Surname,
            DateAdded = employee.DateAdded,
            Gender = (GenderDto)employee.Gender,
            Image = employee.Image
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
}