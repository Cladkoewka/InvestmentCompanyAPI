using Domain.Models;
using Domain.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { } 
    
    public DbSet<Asset> Assets { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Asset>().ToTable("Assets"); // Указываем имя существующей таблицы
        modelBuilder.Entity<User>().ToTable("Users"); // Указываем имя существующей таблицы
        modelBuilder.Entity<Role>().ToTable("Roles"); // Указываем имя существующей таблицы
        
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role) // Связь с ролью
            .WithMany()           // У роли может быть много пользователей
            .HasForeignKey(u => u.RoleId) // Указываем, что внешним ключом является RoleId
            .OnDelete(DeleteBehavior.Restrict); // Можно указать поведение при удалении
    }
}