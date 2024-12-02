using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // Получить все отделы
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentGetDto>>> GetAllAsync()
        {
            var departments = await _departmentService.GetAllAsync();
            return Ok(departments);
        }

        // Получить отдел по ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<DepartmentGetDto>> GetByIdAsync(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);
            if (department == null)
                return NotFound();

            return Ok(department);
        }

        // Получить отдел по имени
        [HttpGet("by-name/{name}")]
        public async Task<ActionResult<DepartmentGetDto>> GetByNameAsync(string name)
        {
            var department = await _departmentService.GetByNameAsync(name);
            if (department == null)
                return NotFound();

            return Ok(department);
        }

        // Добавить новый отдел
        [HttpPost]
        public async Task<ActionResult<DepartmentGetDto>> AddAsync([FromBody] DepartmentCreateDto dto)
        {
            var department = await _departmentService.AddAsync(dto);
            return StatusCode(201, department);
        }

        // Обновить отдел
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] DepartmentUpdateDto dto)
        {
            var isUpdated = await _departmentService.UpdateAsync(id, dto);
            if (!isUpdated)
                return NotFound();

            return NoContent();
        }

        // Удалить отдел
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var isDeleted = await _departmentService.DeleteAsync(id);
            if (!isDeleted)
                return NotFound();

            return NoContent();
        }
    }
}
