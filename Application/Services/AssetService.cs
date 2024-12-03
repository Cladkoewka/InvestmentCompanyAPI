using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IMapper _mapper;

        public AssetService(IAssetRepository assetRepository, IMapper mapper)
        {
            _assetRepository = assetRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AssetGetDto>> GetAllAsync()
        {
            var assets = await _assetRepository.GetAllAsync();
            return assets.Select(asset => _mapper.Map<AssetGetDto>(asset));
        }

        public async Task<AssetGetDto?> GetByIdAsync(int id)
        {
            var asset = await _assetRepository.GetByIdAsync(id);
            if (asset == null)
                return null;

            return _mapper.Map<AssetGetDto>(asset);
        }

        public async Task<AssetGetDto> GetByNameAsync(string name)
        {
            var asset = await _assetRepository.GetByNameAsync(name);
            return _mapper.Map<AssetGetDto>(asset);
        }

        public async Task<AssetGetDto> AddAsync(AssetCreateDto dto)
        {
            var existingAsset = await _assetRepository.GetByNameAsync(dto.Name);
            if (existingAsset != null)
                return _mapper.Map<AssetGetDto>(existingAsset);

            var asset = _mapper.Map<Asset>(dto);
            await _assetRepository.AddAsync(asset);

            return _mapper.Map<AssetGetDto>(asset);
        }

        public async Task<bool> UpdateAsync(int id, AssetUpdateDto dto)
        {
            var existingAsset = await _assetRepository.GetByIdAsync(id);
            if (existingAsset == null)
                return false;

            _mapper.Map(dto, existingAsset);
            await _assetRepository.UpdateAsync(existingAsset);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingAsset = await _assetRepository.GetByIdAsync(id);
            if (existingAsset == null)
                return false;

            await _assetRepository.DeleteAsync(existingAsset);
            return true;
        }
    }
}
