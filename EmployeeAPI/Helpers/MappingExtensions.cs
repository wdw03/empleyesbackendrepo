using EmployeeAPI.DTOs.Auth;
using EmployeeAPI.DTOs.Dashboard;
using EmployeeAPI.DTOs.Department;
using EmployeeAPI.DTOs.Employee;
using EmployeeAPI.DTOs.Role;
using EmployeeAPI.Models;

namespace EmployeeAPI.Helpers;

public static class MappingExtensions
{
    // Employee -> EmployeeDto
    public static EmployeeDto ToDto(this Employee employee)
    {
        return new EmployeeDto
        {
            Id = employee.Id,
            EmployeeCode = employee.EmployeeCode,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            Phone = employee.Phone,
            DateOfBirth = employee.DateOfBirth,
            Gender = employee.Gender.ToString(),
            Address = employee.Address,
            City = employee.City,
            State = employee.State,
            PostalCode = employee.PostalCode,
            DepartmentId = employee.DepartmentId,
            DepartmentName = employee.Department?.Name ?? string.Empty,
            RoleId = employee.RoleId,
            RoleName = employee.Role?.Name ?? string.Empty,
            Designation = employee.Designation,
            DateOfJoining = employee.DateOfJoining,
            Salary = employee.Salary,
            EmploymentType = employee.EmploymentType.ToString(),
            Status = employee.Status.ToString(),
            ProfileImageUrl = employee.ProfileImageUrl,
            CreatedAt = employee.CreatedAt,
            UpdatedAt = employee.UpdatedAt
        };
    }

    // Employee -> EmployeeListDto
    public static EmployeeListDto ToListDto(this Employee employee)
    {
        return new EmployeeListDto
        {
            Id = employee.Id,
            EmployeeCode = employee.EmployeeCode,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            Phone = employee.Phone,
            DepartmentName = employee.Department?.Name ?? string.Empty,
            Designation = employee.Designation,
            DateOfJoining = employee.DateOfJoining,
            Status = employee.Status.ToString(),
            EmploymentType = employee.EmploymentType.ToString(),
            ProfileImageUrl = employee.ProfileImageUrl
        };
    }

    // CreateEmployeeDto -> Employee
    public static Employee ToEntity(this CreateEmployeeDto dto)
    {
        return new Employee
        {
            EmployeeCode = dto.EmployeeCode,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Phone = dto.Phone,
            DateOfBirth = dto.DateOfBirth,
            Gender = dto.Gender,
            Address = dto.Address,
            City = dto.City,
            State = dto.State,
            PostalCode = dto.PostalCode,
            DepartmentId = dto.DepartmentId,
            RoleId = dto.RoleId,
            Designation = dto.Designation,
            DateOfJoining = dto.DateOfJoining,
            Salary = dto.Salary,
            EmploymentType = dto.EmploymentType,
            Status = dto.Status,
            ProfileImageUrl = dto.ProfileImageUrl,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    // UpdateEmployeeDto -> update existing Employee
    public static void UpdateFrom(this Employee employee, UpdateEmployeeDto dto)
    {
        employee.FirstName = dto.FirstName;
        employee.LastName = dto.LastName;
        employee.Email = dto.Email;
        employee.Phone = dto.Phone;
        employee.DateOfBirth = dto.DateOfBirth;
        employee.Gender = dto.Gender;
        employee.Address = dto.Address;
        employee.City = dto.City;
        employee.State = dto.State;
        employee.PostalCode = dto.PostalCode;
        employee.DepartmentId = dto.DepartmentId;
        employee.RoleId = dto.RoleId;
        employee.Designation = dto.Designation;
        employee.DateOfJoining = dto.DateOfJoining;
        employee.Salary = dto.Salary;
        employee.EmploymentType = dto.EmploymentType;
        employee.Status = dto.Status;
        employee.ProfileImageUrl = dto.ProfileImageUrl;
        employee.UpdatedAt = DateTime.UtcNow;
    }

    // Department -> DepartmentDto
    public static DepartmentDto ToDto(this Department department)
    {
        return new DepartmentDto
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description,
            ManagerId = department.ManagerId,
            EmployeeCount = department.Employees?.Count ?? 0,
            CreatedAt = department.CreatedAt,
            UpdatedAt = department.UpdatedAt
        };
    }

    // Role -> RoleDto
    public static RoleDto ToDto(this Models.Role role)
    {
        return new RoleDto
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description,
            CreatedAt = role.CreatedAt,
            UpdatedAt = role.UpdatedAt
        };
    }

    // User -> UserDto
    public static UserDto ToDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role?.Name ?? string.Empty,
            RoleId = user.RoleId,
            EmployeeId = user.EmployeeId,
            IsActive = user.IsActive
        };
    }

    // Employee -> RecentEmployeeDto
    public static RecentEmployeeDto ToRecentDto(this Employee employee)
    {
        return new RecentEmployeeDto
        {
            Id = employee.Id,
            FullName = $"{employee.FirstName} {employee.LastName}",
            Email = employee.Email,
            Department = employee.Department?.Name ?? string.Empty,
            Designation = employee.Designation,
            DateOfJoining = employee.DateOfJoining,
            Status = employee.Status.ToString(),
            ProfileImageUrl = employee.ProfileImageUrl
        };
    }
}
