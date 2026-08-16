using EmployeeAPI.DTOs.Dashboard;

namespace EmployeeAPI.Services;

public interface IDashboardService
{
    Task<DashboardStatsDto> GetStatsAsync();
}
