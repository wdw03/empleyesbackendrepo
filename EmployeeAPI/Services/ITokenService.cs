using EmployeeAPI.DTOs.Auth;
using EmployeeAPI.Models;
using System.Security.Claims;

namespace EmployeeAPI.Services;

public interface ITokenService
{
    string GenerateToken(User user);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}
