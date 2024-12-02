using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditorController : ControllerBase
    {
        private readonly IEditorService _editorService;

        public EditorController(IEditorService editorService)
        {
            _editorService = editorService;
        }

        // Получить всех редакторов
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EditorGetDto>>> GetAllAsync()
        {
            var editors = await _editorService.GetAllAsync();
            return Ok(editors);
        }
        
        // Получить редактора по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<EditorGetDto>> GetByIdAsync(int id)
        {
            var editor = await _editorService.GetByIdAsync(id);
            if (editor == null)
                return NotFound();

            return Ok(editor);
        }

        // Добавить нового редактора
        [HttpPost]
        public async Task<ActionResult<EditorGetDto>> AddAsync([FromBody] EditorCreateDto dto)
        {
            var editor = await _editorService.AddAsync(dto);
            return StatusCode(201, editor);
        }

        // Обновить редактора
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] EditorUpdateDto dto)
        {
            var success = await _editorService.UpdateAsync(id, dto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // Удалить редактора
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var success = await _editorService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // Получить редактора по email
        [HttpGet("by-email/{email}")]
        public async Task<ActionResult<EditorGetDto>> GetByEmailAsync(string email)
        {
            var editor = await _editorService.GetByEmailAsync(email);
            if (editor == null)
                return NotFound();

            return Ok(editor);
        }
    }
}
