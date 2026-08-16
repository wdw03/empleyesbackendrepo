using EmployeeAPI.DTOs.Common;
using EmployeeAPI.DTOs.Employee;
using EmployeeAPI.Helpers;
using EmployeeAPI.Repositories;

namespace EmployeeAPI.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<PagedResult<EmployeeListDto>> GetAllAsync(EmployeeQueryParameters parameters)
    {
        var result = await _employeeRepository.GetAllAsync(parameters);
        return new PagedResult<EmployeeListDto>
        {
            Items = result.Items.Select(e => e.ToListDto()).ToList(),
            Page = result.Page,
            PageSize = result.PageSize,
            TotalItems = result.TotalItems,
            TotalPages = result.TotalPages
        };
    }

    public async Task<EmployeeDto?> GetByIdAsync(int id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        return employee?.ToDto();
    }

    public async Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto)
    {
        // Check uniqueness
        var existingEmail = await _employeeRepository.GetByEmailAsync(dto.Email);
        if (existingEmail != null)
            throw new InvalidOperationException("An employee with this email already exists.");

        var existingCode = await _employeeRepository.GetByCodeAsync(dto.EmployeeCode);
        if (existingCode != null)
            throw new InvalidOperationException("An employee with this code already exists.");

        var employee = dto.ToEntity();
        var created = await _employeeRepository.CreateAsync(employee);
        return created.ToDto();
    }

    public async Task<EmployeeDto?> UpdateAsync(int id, UpdateEmployeeDto dto)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        if (employee == null) return null;

        // Check email uniqueness (exclude current)
        var existingEmail = await _employeeRepository.GetByEmailAsync(dto.Email);
        if (existingEmail != null && existingEmail.Id != id)
            throw new InvalidOperationException("An employee with this email already exists.");

        employee.UpdateFrom(dto);
        var updated = await _employeeRepository.UpdateAsync(employee);
        return updated.ToDto();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _employeeRepository.DeleteAsync(id);
    }
}
