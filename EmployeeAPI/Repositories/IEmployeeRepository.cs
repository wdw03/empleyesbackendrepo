using EmployeeAPI.DTOs.Common;
using EmployeeAPI.Models;

namespace EmployeeAPI.Repositories;

public interface IEmployeeRepository
{
    Task<PagedResult<Employee>> GetAllAsync(EmployeeQueryParameters parameters);
    Task<Employee?> GetByIdAsync(int id);
    Task<Employee?> GetByEmailAsync(string email);
    Task<Employee?> GetByCodeAsync(string code);
    Task<Employee> CreateAsync(Employee employee);
    Task<Employee> UpdateAsync(Employee employee);
    Task<bool> DeleteAsync(int id);
    Task<int> GetCountAsync();
    Task<int> GetCountByStatusAsync(EmployeeStatus status);
    Task<int> GetJoinedThisMonthCountAsync();
    Task<List<Employee>> GetRecentEmployeesAsync(int count);
    IQueryable<Employee> GetQueryable();
}
