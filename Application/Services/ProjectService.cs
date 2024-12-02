using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        // Получить проект по ID
        public async Task<ProjectGetDto?> GetByIdAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
                return null;

            return _mapper.Map<ProjectGetDto>(project);
        }

        // Получить все проекты
        public async Task<IEnumerable<ProjectGetDto>> GetAllAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return projects.Select(project => _mapper.Map<ProjectGetDto>(project));
        }

        // Добавить новый проект
        public async Task<ProjectGetDto> AddAsync(ProjectCreateDto dto)
        {
            var project = _mapper.Map<Project>(dto);
            await _projectRepository.AddAsync(project);

            return _mapper.Map<ProjectGetDto>(project);
        }

        // Обновить проект
        public async Task<bool> UpdateAsync(int id, ProjectUpdateDto dto)
        {
            var existingProject = await _projectRepository.GetByIdAsync(id);
            if (existingProject == null)
                return false;

            // Маппим DTO в сущность, сохраняя существующий объект
            _mapper.Map(dto, existingProject);
            await _projectRepository.UpdateAsync(existingProject);
            return true;
        }

        // Удалить проект
        public async Task<bool> DeleteAsync(int id)
        {
            var existingProject = await _projectRepository.GetByIdAsync(id);
            if (existingProject == null)
                return false;

            await _projectRepository.DeleteAsync(existingProject);
            return true;
        }

        // Получить проекты по CustomerId
        public async Task<IEnumerable<ProjectGetDto>> GetByCustomerIdAsync(int customerId)
        {
            var projects = await _projectRepository.GetByCustomerIdAsync(customerId);
            return projects.Select(project => _mapper.Map<ProjectGetDto>(project));
        }

        // Получить проекты по EditorId
        public async Task<IEnumerable<ProjectGetDto>> GetByEditorIdAsync(int editorId)
        {
            var projects = await _projectRepository.GetByEditorIdAsync(editorId);
            return projects.Select(project => _mapper.Map<ProjectGetDto>(project));
        }
    }
}
