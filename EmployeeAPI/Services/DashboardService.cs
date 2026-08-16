using EmployeeAPI.DTOs.Dashboard;
using EmployeeAPI.Helpers;
using EmployeeAPI.Models;
using EmployeeAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Services;

public class DashboardService : IDashboardService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IDepartmentRepository _departmentRepository;

    public DashboardService(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
    {
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
    }

    public async Task<DashboardStatsDto> GetStatsAsync()
    {
        var queryable = _employeeRepository.GetQueryable();

        var totalEmployees = await _employeeRepository.GetCountAsync();
        var activeEmployees = await _employeeRepository.GetCountByStatusAsync(EmployeeStatus.Active);
        var inactiveEmployees = await _employeeRepository.GetCountByStatusAsync(EmployeeStatus.Inactive);
        var onLeaveEmployees = await _employeeRepository.GetCountByStatusAsync(EmployeeStatus.OnLeave);
        var totalDepartments = await _departmentRepository.GetCountAsync();
        var joinedThisMonth = await _employeeRepository.GetJoinedThisMonthCountAsync();

        var employeesByDepartment = await queryable
            .GroupBy(e => e.Department.Name)
            .Select(g => new DepartmentEmployeeCount
            {
                Department = g.Key,
                Count = g.Count()
            })
            .ToListAsync();

        var employeesByType = await queryable
            .GroupBy(e => e.EmploymentType)
            .Select(g => new EmploymentTypeCount
            {
                Type = g.Key.ToString(),
                Count = g.Count()
            })
            .ToListAsync();

        var employeesByStatus = await queryable
            .GroupBy(e => e.Status)
            .Select(g => new StatusCount
            {
                Status = g.Key.ToString(),
                Count = g.Count()
            })
            .ToListAsync();

        var recentEmployees = await _employeeRepository.GetRecentEmployeesAsync(5);

        return new DashboardStatsDto
        {
            TotalEmployees = totalEmployees,
            ActiveEmployees = activeEmployees,
            InactiveEmployees = inactiveEmployees,
            OnLeaveEmployees = onLeaveEmployees,
            TotalDepartments = totalDepartments,
            EmployeesJoinedThisMonth = joinedThisMonth,
            EmployeesByDepartment = employeesByDepartment,
            EmployeesByEmploymentType = employeesByType,
            EmployeesByStatus = employeesByStatus,
            RecentEmployees = recentEmployees.Select(e => e.ToRecentDto()).ToList()
        };
    }
}
