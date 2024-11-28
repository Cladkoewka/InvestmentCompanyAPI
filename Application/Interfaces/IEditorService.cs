using Application.DTOs;

namespace Application.Interfaces;

public interface IEditorService : IService<EditorGetDto, EditorCreateDto, EditorUpdateDto>
{
    Task<EditorGetDto?> GetByEmailAsync(string email);
}