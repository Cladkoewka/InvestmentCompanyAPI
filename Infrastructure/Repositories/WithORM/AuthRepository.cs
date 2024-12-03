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

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users
            .Include(u => u.Role)  
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        return await _context.Users
            .Include(u => u.Role)  
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}