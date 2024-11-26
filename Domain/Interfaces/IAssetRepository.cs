using Domain.Models;

namespace Domain.Interfaces;

public interface IAssetRepository : IRepository<Asset>
{
    Task<IEnumerable<Asset>> GetByNameAsync(string name);
}