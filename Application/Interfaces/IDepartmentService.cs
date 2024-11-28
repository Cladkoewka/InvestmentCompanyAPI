using Application.DTOs;

namespace Application.Interfaces;

public interface IDepartmentService : IService<DepartmentGetDto, DepartmentCreateDto, DepartmentUpdateDto>
{
    Task<DepartmentGetDto?> GetByNameAsync(string name);
}