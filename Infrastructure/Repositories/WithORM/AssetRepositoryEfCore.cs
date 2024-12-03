using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Domain.Interfaces;

namespace Infrastructure.Repositories.WithORM
{
    public class AssetRepositoryEfCore : IAssetRepository
    {
        private readonly ApplicationDbContext _context;

        public AssetRepositoryEfCore(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Asset>> GetAllAsync()
        {
            return await _context.Assets.ToListAsync();
        }

        public async Task<Asset?> GetByIdAsync(int id)
        {
            return await _context.Assets
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Asset?> GetByNameAsync(string name)
        {
            return await _context.Assets
                .FirstOrDefaultAsync(a => a.Name == name);
        }

        public async Task AddAsync(Asset entity)
        {
            await _context.Assets.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Asset entity)
        {
            _context.Assets.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Asset entity)
        {
            _context.Assets.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}