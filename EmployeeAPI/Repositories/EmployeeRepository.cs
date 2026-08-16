using EmployeeAPI.Data;
using EmployeeAPI.DTOs.Common;
using EmployeeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<Employee>> GetAllAsync(EmployeeQueryParameters parameters)
    {
        var query = _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Role)
            .AsQueryable();

        // Search
        if (!string.IsNullOrWhiteSpace(parameters.Search))
        {
            var search = parameters.Search.ToLower();
            query = query.Where(e =>
                e.FirstName.ToLower().Contains(search) ||
                e.LastName.ToLower().Contains(search) ||
                e.Email.ToLower().Contains(search) ||
                e.EmployeeCode.ToLower().Contains(search) ||
                (e.Designation != null && e.Designation.ToLower().Contains(search)));
        }

        // Filter by Department
        if (parameters.DepartmentId.HasValue)
        {
            query = query.Where(e => e.DepartmentId == parameters.DepartmentId.Value);
        }

        // Filter by Role
        if (parameters.RoleId.HasValue)
        {
            query = query.Where(e => e.RoleId == parameters.RoleId.Value);
        }

        // Filter by Status
        if (!string.IsNullOrWhiteSpace(parameters.Status) &&
            Enum.TryParse<EmployeeStatus>(parameters.Status, true, out var status))
        {
            query = query.Where(e => e.Status == status);
        }

        // Filter by Employment Type
        if (!string.IsNullOrWhiteSpace(parameters.EmploymentType) &&
            Enum.TryParse<EmploymentType>(parameters.EmploymentType, true, out var empType))
        {
            query = query.Where(e => e.EmploymentType == empType);
        }

        // Get total before pagination
        var totalItems = await query.CountAsync();

        // Sorting
        query = parameters.SortBy?.ToLower() switch
        {
            "firstname" => parameters.SortOrder.ToLower() == "desc"
                ? query.OrderByDescending(e => e.FirstName)
                : query.OrderBy(e => e.FirstName),
            "lastname" => parameters.SortOrder.ToLower() == "desc"
                ? query.OrderByDescending(e => e.LastName)
                : query.OrderBy(e => e.LastName),
            "email" => parameters.SortOrder.ToLower() == "desc"
                ? query.OrderByDescending(e => e.Email)
                : query.OrderBy(e => e.Email),
            "department" => parameters.SortOrder.ToLower() == "desc"
                ? query.OrderByDescending(e => e.Department.Name)
                : query.OrderBy(e => e.Department.Name),
            "dateofjoining" => parameters.SortOrder.ToLower() == "desc"
                ? query.OrderByDescending(e => e.DateOfJoining)
                : query.OrderBy(e => e.DateOfJoining),
            "salary" => parameters.SortOrder.ToLower() == "desc"
                ? query.OrderByDescending(e => e.Salary)
                : query.OrderBy(e => e.Salary),
            "employeecode" => parameters.SortOrder.ToLower() == "desc"
                ? query.OrderByDescending(e => e.EmployeeCode)
                : query.OrderBy(e => e.EmployeeCode),
            _ => query.OrderByDescending(e => e.CreatedAt)
        };

        // Pagination
        var items = await query
            .Skip((parameters.Page - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .ToListAsync();

        return new PagedResult<Employee>
        {
            Items = items,
            Page = parameters.Page,
            PageSize = parameters.PageSize,
            TotalItems = totalItems,
            TotalPages = (int)Math.Ceiling(totalItems / (double)parameters.PageSize)
        };
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Role)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Employee?> GetByEmailAsync(string email)
    {
        return await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
    }

    public async Task<Employee?> GetByCodeAsync(string code)
    {
        return await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeCode == code);
    }

    public async Task<Employee> CreateAsync(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        return await GetByIdAsync(employee.Id) ?? employee;
    }

    public async Task<Employee> UpdateAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();

        return await GetByIdAsync(employee.Id) ?? employee;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return false;

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<int> GetCountAsync()
    {
        return await _context.Employees.CountAsync();
    }

    public async Task<int> GetCountByStatusAsync(EmployeeStatus status)
    {
        return await _context.Employees.CountAsync(e => e.Status == status);
    }

    public async Task<int> GetJoinedThisMonthCountAsync()
    {
        var startOfMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
        return await _context.Employees.CountAsync(e => e.DateOfJoining >= startOfMonth);
    }

    public async Task<List<Employee>> GetRecentEmployeesAsync(int count)
    {
        return await _context.Employees
            .Include(e => e.Department)
            .OrderByDescending(e => e.DateOfJoining)
            .Take(count)
            .ToListAsync();
    }

    public IQueryable<Employee> GetQueryable()
    {
        return _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Role);
    }
}
