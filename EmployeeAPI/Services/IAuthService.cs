using EmployeeAPI.DTOs.Auth;

namespace EmployeeAPI.Services;

public interface IAuthService
{
    Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
    Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto);
    Task<AuthResponseDto?> RefreshTokenAsync(RefreshTokenDto refreshTokenDto);
    Task<UserDto?> GetCurrentUserAsync(int userId);
}
