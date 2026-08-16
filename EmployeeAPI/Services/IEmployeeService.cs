using EmployeeAPI.DTOs.Common;
using EmployeeAPI.DTOs.Employee;

namespace EmployeeAPI.Services;

public interface IEmployeeService
{
    Task<PagedResult<EmployeeListDto>> GetAllAsync(EmployeeQueryParameters parameters);
    Task<EmployeeDto?> GetByIdAsync(int id);
    Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto);
    Task<EmployeeDto?> UpdateAsync(int id, UpdateEmployeeDto dto);
    Task<bool> DeleteAsync(int id);
}
