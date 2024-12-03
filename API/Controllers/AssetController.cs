using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IAssetService _assetService;

        public AssetController(IAssetService assetService)
        {
            _assetService = assetService;
        }
        
        // Получить все активы
        [Authorize(Roles = "Admin,Viewer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetGetDto>>> GetAllAsync()
        {
            var assets = await _assetService.GetAllAsync();
            return Ok(assets); // Возвращаем список активов
        }

        // Получить актив по ID
        [Authorize(Roles = "Admin,Viewer")]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AssetGetDto>> GetByIdAsync(int id)
        {
            var asset = await _assetService.GetByIdAsync(id);
            if (asset == null)
                return NotFound(); // Возвращаем 404, если актив не найден

            return Ok(asset); // Возвращаем найденный актив
        }

        

        // Добавить новый актив
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<AssetGetDto>> AddAsync([FromBody] AssetCreateDto dto)
        {
            // Проверка валидации
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var asset = await _assetService.AddAsync(dto);
            return StatusCode(201, asset);
        }

        // Обновить актив
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] AssetUpdateDto dto)
        {
            // Проверка валидации
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var isUpdated = await _assetService.UpdateAsync(id, dto);
            if (!isUpdated)
                return NotFound(); // Возвращаем 404, если актив не найден

            return NoContent(); // Возвращаем статус 204 (без контента) при успешном обновлении
        }

        // Удалить актив
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var isDeleted = await _assetService.DeleteAsync(id);
            if (!isDeleted)
                return NotFound(); // Возвращаем 404, если актив не найден

            return NoContent(); // Возвращаем статус 204 при успешном удалении
        }

        // Получить активы по имени
        [Authorize(Roles = "Admin,Viewer")]
        [HttpGet("by-name/{name}")]
        public async Task<ActionResult<IEnumerable<AssetGetDto>>> GetByNameAsync(string name)
        {
            var assets = await _assetService.GetByNameAsync(name);
            return Ok(assets); 
        }
    }
}
