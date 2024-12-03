using Application.DTOs.Auth;

namespace Application.Interfaces.Auth;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterDto registerDto);
    Task<string> LoginAsync(LoginDto loginDto);
}