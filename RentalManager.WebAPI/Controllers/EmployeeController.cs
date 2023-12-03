﻿using Microsoft.AspNetCore.Mvc;
using RentalManager.Infrastructure.Commands.EmployeeCommands;
using RentalManager.Infrastructure.DTO;
using RentalManager.Infrastructure.Services.Interfaces;

// ReSharper disable RouteTemplates.RouteParameterConstraintNotResolved

namespace RentalManager.WebAPI.Controllers;

[ApiController]
[Route("[Controller]")]
public class EmployeeController : Controller
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [ProducesResponseType(typeof(EmployeeDto), 200)]
    [HttpPost]
    public async Task<IActionResult> AddEmployee([FromForm] CreateEmployee createEmployee)
    {
        var result = await _employeeService.AddAsync(createEmployee);

        return Json(result);
    }

    [ProducesResponseType(typeof(IEnumerable<EmployeeDto>), 200)]
    [HttpGet]
    public async Task<IActionResult> BrowseAllEmployees(string? name = null,
        DateTime? from = null,
        DateTime? to = null)
    {
        var result = await _employeeService.BrowseAllAsync(name, from, to);

        return Json(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        await _employeeService.DeleteAsync(id);

        return NoContent();
    }

    [ProducesResponseType(typeof(EmployeeDto), 200)]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetEmployee(int id)
    {
        var clientDto = await _employeeService.GetAsync(id);

        return Json(clientDto);
    }

    [ProducesResponseType(typeof(EmployeeDto), 200)]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateEmployee([FromForm] UpdateEmployee updateEmployee,
        int id)
    {
        var result = await _employeeService.UpdateAsync(updateEmployee, id);

        return Json(result);
    }

    [ProducesResponseType(typeof(File), 200)]
    [Route("/Employee/Image/{id}")]
    [HttpGet]
    public async Task<IActionResult?> GetEmployeeImage(int id)
    {
        var employeeDto = await _employeeService.GetAsync(id);

        if (employeeDto.Image is null)
        {
            return File(
                employeeDto.Gender == GenderDto.Man
                    ? "~/DefaultUserImageMan.png"
                    : "~/DefaultUserImageWoman.png", "image/png");
        }

        return File(employeeDto.Image, "image/jpeg");
    }
}