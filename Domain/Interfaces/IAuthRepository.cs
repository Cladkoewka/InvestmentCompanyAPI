using Domain.Models.Auth;

namespace Domain.Interfaces;

public interface IAuthRepository
{
    Task<User?> GetUserByIdAsync(int userId);
    Task<User?> GetUserByEmailAsync(string email);
    Task CreateUserAsync(User user);
}