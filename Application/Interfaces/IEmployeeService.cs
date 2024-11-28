using Application.DTOs;

namespace Application.Interfaces;

public interface IEmployeeService : IService<EmployeeGetDto, EmployeeCreateDto, EmployeeUpdateDto>
{
    Task<IEnumerable<EmployeeGetDto>> GetByDepartmentIdAsync(int departmentId);
}