using Domain.Models;

namespace Domain.Interfaces;

public interface IRiskRepository : IRepository<Risk>
{
    Task<IEnumerable<Risk>> GetByGradeAsync(int grade);
}