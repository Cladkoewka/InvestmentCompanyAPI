using Application.DTOs;

namespace Application.Interfaces;

public interface IAssetService : IService<AssetGetDto, AssetCreateDto, AssetUpdateDto>
{
    Task<IEnumerable<AssetGetDto>> GetByNameAsync(string name);
}