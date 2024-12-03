using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class EditorService : IEditorService
    {
        private readonly IEditorRepository _editorRepository;
        private readonly IMapper _mapper;

        public EditorService(IEditorRepository editorRepository, IMapper mapper)
        {
            _editorRepository = editorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EditorGetDto>> GetAllAsync()
        {
            var editors = await _editorRepository.GetAllAsync();
            return editors.Select(editor => _mapper.Map<EditorGetDto>(editor));
        }

        public async Task<EditorGetDto?> GetByIdAsync(int id)
        {
            var editor = await _editorRepository.GetByIdAsync(id);
            if (editor == null)
                return null;

            return _mapper.Map<EditorGetDto>(editor);
        }

        public async Task<EditorGetDto?> GetByEmailAsync(string email)
        {
            var editor = await _editorRepository.GetByEmailAsync(email);
            if (editor == null)
                return null;

            return _mapper.Map<EditorGetDto>(editor);
        }

        public async Task<EditorGetDto> AddAsync(EditorCreateDto dto)
        {
            var existingEditor = await _editorRepository.GetByEmailAsync(dto.Email);
            if (existingEditor != null)
                return _mapper.Map<EditorGetDto>(existingEditor);

            var editor = _mapper.Map<Editor>(dto);
            await _editorRepository.AddAsync(editor);

            return _mapper.Map<EditorGetDto>(editor);
        }

        public async Task<bool> UpdateAsync(int id, EditorUpdateDto dto)
        {
            var existingEditor = await _editorRepository.GetByIdAsync(id);
            if (existingEditor == null)
                return false;

            _mapper.Map(dto, existingEditor);
            await _editorRepository.UpdateAsync(existingEditor);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingEditor = await _editorRepository.GetByIdAsync(id);
            if (existingEditor == null)
                return false;

            await _editorRepository.DeleteAsync(existingEditor);
            return true;
        }
    }
}
