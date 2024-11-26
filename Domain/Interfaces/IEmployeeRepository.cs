using Domain.Models;

namespace Domain.Interfaces;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<IEnumerable<Employee>> GetByDepartmentIdAsync(int departmentId);
}