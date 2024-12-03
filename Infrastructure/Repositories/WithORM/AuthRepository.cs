using Domain.Interfaces;
using Domain.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.WithORM;

public class AuthRepository : IAuthRepository
{
    private readonly ApplicationDbContext _context;


    public AuthRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Получение пользователя по email
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users
            .Include(u => u.Role)  // Загружаем роль пользователя
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    // Получение пользователя по Id
    public async Task<User?> GetUserByIdAsync(int userId)
    {
        return await _context.Users
            .Include(u => u.Role)  // Загружаем роль пользователя
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    // Создание нового пользователя
    public async Task CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}