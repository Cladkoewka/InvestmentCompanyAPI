using Domain.Models;

namespace Domain.Interfaces;

public interface IDepartmentRepository : IRepository<Department>
{
    Task<Department?> GetByNameAsync(string name);
}