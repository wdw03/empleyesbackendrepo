namespace EmployeeAPI.DTOs.Common;

public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
}

public class EmployeeQueryParameters
{
    private int _pageSize = 10;
    private int _page = 1;

    public int Page
    {
        get => _page;
        set => _page = value < 1 ? 1 : value;
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > 50 ? 50 : (value < 1 ? 1 : value);
    }

    public string? Search { get; set; }
    public int? DepartmentId { get; set; }
    public int? RoleId { get; set; }
    public string? Status { get; set; }
    public string? EmploymentType { get; set; }
    public string? SortBy { get; set; }
    public string SortOrder { get; set; } = "asc";
}
