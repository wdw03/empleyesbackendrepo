namespace EmployeeAPI.DTOs.Dashboard;

public class DashboardStatsDto
{
    public int TotalEmployees { get; set; }
    public int ActiveEmployees { get; set; }
    public int InactiveEmployees { get; set; }
    public int OnLeaveEmployees { get; set; }
    public int TotalDepartments { get; set; }
    public int EmployeesJoinedThisMonth { get; set; }
    public List<DepartmentEmployeeCount> EmployeesByDepartment { get; set; } = new();
    public List<EmploymentTypeCount> EmployeesByEmploymentType { get; set; } = new();
    public List<StatusCount> EmployeesByStatus { get; set; } = new();
    public List<RecentEmployeeDto> RecentEmployees { get; set; } = new();
}

public class DepartmentEmployeeCount
{
    public string Department { get; set; } = string.Empty;
    public int Count { get; set; }
}

public class EmploymentTypeCount
{
    public string Type { get; set; } = string.Empty;
    public int Count { get; set; }
}

public class StatusCount
{
    public string Status { get; set; } = string.Empty;
    public int Count { get; set; }
}

public class RecentEmployeeDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string? Designation { get; set; }
    public DateTime DateOfJoining { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? ProfileImageUrl { get; set; }
}
