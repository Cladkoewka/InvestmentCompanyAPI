using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RiskController : ControllerBase
    {
        private readonly IRiskService _riskService;

        public RiskController(IRiskService riskService)
        {
            _riskService = riskService;
        }

        // Получить все риски
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RiskGetDto>>> GetAllAsync()
        {
            var risks = await _riskService.GetAllAsync();
            return Ok(risks);
        }
        
        // Получить риск по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<RiskGetDto>> GetByIdAsync(int id)
        {
            var risk = await _riskService.GetByIdAsync(id);
            if (risk == null)
                return NotFound();

            return Ok(risk);
        }
        

        // Добавить новый риск
        [HttpPost]
        public async Task<ActionResult<RiskGetDto>> AddAsync([FromBody] RiskCreateDto dto)
        {
            // Проверка валидации
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var newRisk = await _riskService.AddAsync(dto);
            return StatusCode(201, newRisk);
        }

        // Обновить риск
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] RiskUpdateDto dto)
        {
            // Проверка валидации
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = await _riskService.UpdateAsync(id, dto);
            if (!result)
                return NotFound();

            return NoContent();
        }

        // Удалить риск
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await _riskService.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        // Получить риски по grade
        [HttpGet("by-grade/{grade}")]
        public async Task<ActionResult<IEnumerable<RiskGetDto>>> GetByGradeAsync(int grade)
        {
            var risks = await _riskService.GetByGradeAsync(grade);
            return Ok(risks);
        }
    }
}
