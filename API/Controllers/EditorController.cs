using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin,Viewer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EditorGetDto>>> GetAllAsync()
        {
            var editors = await _editorService.GetAllAsync();
            return Ok(editors);
        }
        
        [Authorize(Roles = "Admin,Viewer")]
        [HttpGet("{id}")]
        public async Task<ActionResult<EditorGetDto>> GetByIdAsync(int id)
        {
            var editor = await _editorService.GetByIdAsync(id);
            if (editor == null)
                return NotFound();

            return Ok(editor);
        }

        [Authorize(Roles = "Admin,Viewer")]
        [HttpGet("by-email/{email}")]
        public async Task<ActionResult<EditorGetDto>> GetByEmailAsync(string email)
        {
            var editor = await _editorService.GetByEmailAsync(email);
            if (editor == null)
                return NotFound();

            return Ok(editor);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<EditorGetDto>> AddAsync([FromBody] EditorCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var editor = await _editorService.AddAsync(dto);
            return StatusCode(201, editor);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] EditorUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var success = await _editorService.UpdateAsync(id, dto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var success = await _editorService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
