using Application.DTOs;

namespace Application.Interfaces;

public interface IProjectService : IService<ProjectGetDto, ProjectCreateDto, ProjectUpdateDto>
{
    Task<IEnumerable<ProjectGetDto>> GetByCustomerIdAsync(int customerId);
    Task<IEnumerable<ProjectGetDto>> GetByEditorIdAsync(int editorId);
}