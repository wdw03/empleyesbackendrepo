using EmployeeAPI.DTOs.Role;
using EmployeeAPI.Helpers;
using EmployeeAPI.Models;
using EmployeeAPI.Repositories;

namespace EmployeeAPI.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<List<RoleDto>> GetAllAsync()
    {
        var roles = await _roleRepository.GetAllAsync();
        return roles.Select(r => r.ToDto()).ToList();
    }

    public async Task<RoleDto?> GetByIdAsync(int id)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        return role?.ToDto();
    }

    public async Task<RoleDto> CreateAsync(CreateRoleDto dto)
    {
        var existing = await _roleRepository.GetByNameAsync(dto.Name);
        if (existing != null)
            throw new InvalidOperationException("A role with this name already exists.");

        var role = new Role
        {
            Name = dto.Name,
            Description = dto.Description,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var created = await _roleRepository.CreateAsync(role);
        return created.ToDto();
    }

    public async Task<RoleDto?> UpdateAsync(int id, UpdateRoleDto dto)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role == null) return null;

        var existing = await _roleRepository.GetByNameAsync(dto.Name);
        if (existing != null && existing.Id != id)
            throw new InvalidOperationException("A role with this name already exists.");

        role.Name = dto.Name;
        role.Description = dto.Description;
        role.UpdatedAt = DateTime.UtcNow;

        var updated = await _roleRepository.UpdateAsync(role);
        return updated.ToDto();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _roleRepository.DeleteAsync(id);
    }
}
