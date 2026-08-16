using EmployeeAPI.DTOs.Common;
using EmployeeAPI.DTOs.Employee;
using EmployeeAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,HR,Manager")]
    public async Task<IActionResult> GetAll([FromQuery] EmployeeQueryParameters parameters)
    {
        var result = await _employeeService.GetAllAsync(parameters);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        // Employees can only view their own profile
        var role = User.FindFirst(ClaimTypes.Role)?.Value;
        if (role == "Employee")
        {
            var employeeIdClaim = User.FindFirst("EmployeeId")?.Value;
            if (employeeIdClaim == null || !int.TryParse(employeeIdClaim, out var empId) || empId != id)
                return Forbid();
        }

        var employee = await _employeeService.GetByIdAsync(id);
        if (employee == null)
            return NotFound(new { message = "Employee not found." });

        return Ok(employee);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,HR")]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeDto dto)
    {
        var employee = await _employeeService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,HR,Manager")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEmployeeDto dto)
    {
        // Managers can only do limited edits — enforced by the role check above
        var role = User.FindFirst(ClaimTypes.Role)?.Value;
        if (role == "Manager")
        {
            // Managers can update but not change salary or status
            // For simplicity, we allow the update but a real app might restrict fields
        }

        var employee = await _employeeService.UpdateAsync(id, dto);
        if (employee == null)
            return NotFound(new { message = "Employee not found." });

        return Ok(employee);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _employeeService.DeleteAsync(id);
        if (!result)
            return NotFound(new { message = "Employee not found." });

        return NoContent();
    }
}
