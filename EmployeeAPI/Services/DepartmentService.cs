using EmployeeAPI.DTOs.Department;
using EmployeeAPI.Helpers;
using EmployeeAPI.Models;
using EmployeeAPI.Repositories;

namespace EmployeeAPI.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentService(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<List<DepartmentDto>> GetAllAsync()
    {
        var departments = await _departmentRepository.GetAllAsync();
        return departments.Select(d => d.ToDto()).ToList();
    }

    public async Task<DepartmentDto?> GetByIdAsync(int id)
    {
        var department = await _departmentRepository.GetByIdAsync(id);
        return department?.ToDto();
    }

    public async Task<DepartmentDto> CreateAsync(CreateDepartmentDto dto)
    {
        var existing = await _departmentRepository.GetByNameAsync(dto.Name);
        if (existing != null)
            throw new InvalidOperationException("A department with this name already exists.");

        var department = new Department
        {
            Name = dto.Name,
            Description = dto.Description,
            ManagerId = dto.ManagerId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var created = await _departmentRepository.CreateAsync(department);
        return created.ToDto();
    }

    public async Task<DepartmentDto?> UpdateAsync(int id, UpdateDepartmentDto dto)
    {
        var department = await _departmentRepository.GetByIdAsync(id);
        if (department == null) return null;

        var existing = await _departmentRepository.GetByNameAsync(dto.Name);
        if (existing != null && existing.Id != id)
            throw new InvalidOperationException("A department with this name already exists.");

        department.Name = dto.Name;
        department.Description = dto.Description;
        department.ManagerId = dto.ManagerId;
        department.UpdatedAt = DateTime.UtcNow;

        var updated = await _departmentRepository.UpdateAsync(department);
        return updated.ToDto();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _departmentRepository.DeleteAsync(id);
    }
}
