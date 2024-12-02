using Application.DTOs;

namespace Application.Interfaces;

public interface IAssetService : IService<AssetGetDto, AssetCreateDto, AssetUpdateDto>
{
    Task<AssetGetDto> GetByNameAsync(string name);
}