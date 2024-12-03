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

        modelBuilder.Entity<Asset>().ToTable("Assets"); 
        modelBuilder.Entity<User>().ToTable("Users"); 
        modelBuilder.Entity<Role>().ToTable("Roles"); 
        
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany()           
            .HasForeignKey(u => u.RoleId) 
            .OnDelete(DeleteBehavior.Restrict); 
    }
}