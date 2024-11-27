using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using Domain.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EditorController : ControllerBase
    {
        private readonly IEditorRepository _editorRepository;

        public EditorController(IEditorRepository editorRepository)
        {
            _editorRepository = editorRepository;
        }

        [HttpGet("test")]
        public async Task<IActionResult> TestEditorRepositoryMethods()
        {
            var result = string.Empty;

            // Тестирование GetByIdAsync
            var editorById = await _editorRepository.GetByIdAsync(1);
            result += editorById is not null 
                ? $"Editor by ID: {editorById.FullName}" 
                : "Editor not found";
            result += "<br>------------<br>";
            
            // Тестирование GetAllAsync
            var allEditors = await _editorRepository.GetAllAsync();
            result += "<br>All editors:<br>";
            foreach (var editor in allEditors)
            {
                result += $"Editor: {editor.FullName} ({editor.Email})<br>";
            }
            result += "<br>------------<br>";

            // Тестирование AddAsync
            var newEditor = new Editor { FullName = "John Doe", Email = "john.doe@example.com", PhoneNumber = "123-456-7890" };
            await _editorRepository.AddAsync(newEditor);
            result += "Added new editor<br>";
            
            allEditors = await _editorRepository.GetAllAsync();
            result += "<br>All editors after adding:<br>";
            foreach (var editor in allEditors)
            {
                result += $"Editor: {editor.FullName} ({editor.Email})<br>";
            }
            result += "<br>------------<br>";

            // Тестирование UpdateAsync
            var editorToUpdate = new Editor { Id = 1, FullName = "John Updated", Email = "john.updated@example.com", PhoneNumber = "987-654-3210" };
            await _editorRepository.UpdateAsync(editorToUpdate);
            result += "Updated editor<br>";
            
            allEditors = await _editorRepository.GetAllAsync();
            result += "<br>All editors after update:<br>";
            foreach (var editor in allEditors)
            {
                result += $"Editor: {editor.FullName} ({editor.Email})<br>";
            }
            result += "<br>------------<br>";

            // Тестирование DeleteAsync
            await _editorRepository.DeleteAsync(editorToUpdate);
            result += "Deleted editor<br>";
            
            allEditors = await _editorRepository.GetAllAsync();
            result += "<br>All editors after deletion:<br>";
            foreach (var editor in allEditors)
            {
                result += $"Editor: {editor.FullName} ({editor.Email})<br>";
            }
            result += "<br>------------<br>";

            // Тестирование GetByEmailAsync
            var editorByEmail = await _editorRepository.GetByEmailAsync("john.doe@example.com");
            result += "Found editor by email:<br>";
            if (editorByEmail != null)
            {
                result += $"Found editor by email: {editorByEmail.FullName} ({editorByEmail.Email})<br>";
            }
            else
            {
                result += "Editor not found by email.<br>";
            }

            return Content(result, "text/html");
        }
    }
}
