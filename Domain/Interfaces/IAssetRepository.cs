using Domain.Models;

namespace Domain.Interfaces;

public interface IAssetRepository : IRepository<Asset>
{
    Task<Asset> GetByNameAsync(string name);
}