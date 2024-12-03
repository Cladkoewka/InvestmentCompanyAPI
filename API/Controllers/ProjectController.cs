using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        
        // Получить все проекты
        [Authorize(Roles = "Admin,Viewer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectGetDto>>> GetAllAsync()
        {
            var projects = await _projectService.GetAllAsync();
            return Ok(projects);
        }

        // Получить проект по ID
        [Authorize(Roles = "Admin,Viewer")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectGetDto>> GetByIdAsync(int id)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project == null)
                return NotFound();

            return Ok(project);
        }
        

        // Добавить новый проект
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ProjectGetDto>> AddAsync([FromBody] ProjectCreateDto dto)
        {
            // Проверка валидации
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var project = await _projectService.AddAsync(dto);
            return StatusCode(201, project);
        }

        // Обновить проект
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] ProjectUpdateDto dto)
        {
            // Проверка валидации
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var success = await _projectService.UpdateAsync(id, dto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // Удалить проект
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var success = await _projectService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // Получить проекты по CustomerId
        [Authorize(Roles = "Admin,Viewer")]
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<ProjectGetDto>>> GetByCustomerIdAsync(int customerId)
        {
            var projects = await _projectService.GetByCustomerIdAsync(customerId);
            return Ok(projects);
        }

        // Получить проекты по EditorId
        [Authorize(Roles = "Admin,Viewer")]
        [HttpGet("editor/{editorId}")]
        public async Task<ActionResult<IEnumerable<ProjectGetDto>>> GetByEditorIdAsync(int editorId)
        {
            var projects = await _projectService.GetByEditorIdAsync(editorId);
            return Ok(projects);
        }
    }
}
