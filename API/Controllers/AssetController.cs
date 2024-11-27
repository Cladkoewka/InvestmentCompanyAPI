using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using Domain.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetController : ControllerBase
    {
        private readonly IAssetRepository _assetRepository;

        public AssetController(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        [HttpGet("test")]
        public async Task<IActionResult> TestAssetRepositoryMethods()
        {
            // Тестирование GetByIdAsync
            var assetById = await _assetRepository.GetByIdAsync(1);
            var result = assetById is not null 
                ? $"Asset by ID: {assetById.Name}" 
                : "Asset not found";
            result += "<br>------------<br>";
            
            // Тестирование GetAllAsync
            var allAssets = await _assetRepository.GetAllAsync();
            result += "<br>All assets:<br>";
            foreach (var asset in allAssets)
            {
                result += $"Asset: {asset.Name}<br>";
            }
            result += "<br>------------<br>";


            // Тестирование AddAsync
            var newAsset = new Asset { Name = "New Asset" };
            await _assetRepository.AddAsync(newAsset);
            result += "Added new asset<br>";
            
            
            allAssets = await _assetRepository.GetAllAsync();
            result += "<br>All assets:<br>";
            foreach (var asset in allAssets)
            {
                result += $"Asset: {asset.Name}<br>";
            }
            result += "<br>------------<br>";

            // Тестирование UpdateAsync
            var assetToUpdate = new Asset { Id = 1, Name = "Updated Asset" };
            await _assetRepository.UpdateAsync(assetToUpdate);
            result += "Updated asset<br>";
            
            allAssets = await _assetRepository.GetAllAsync();
            result += "<br>All assets:<br>";
            foreach (var asset in allAssets)
            {
                result += $"Asset: {asset.Name}<br>";
            }
            result += "<br>------------<br>";

            
            await _assetRepository.DeleteAsync(assetToUpdate);
            result += "Deleted asset<br>";
            
            allAssets = await _assetRepository.GetAllAsync();
            result += "<br>All assets:<br>";
            foreach (var asset in allAssets)
            {
                result += $"Asset: {asset.Name}<br>";
            }
            result += "<br>------------<br>";

            // Тестирование GetByNameAsync
            var assetsByName = await _assetRepository.GetByNameAsync("New Asset");
            result += "Found asset by name:<br>";
            foreach (var asset in assetsByName)
            {
                result += $"Found asset by name: {asset.Name}<br>";
            }


            
            return Content(result, "text/html");
        }
    }
}
