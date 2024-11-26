using Domain.Models;

namespace Domain.Interfaces;

public interface IEditorRepository : IRepository<Editor>
{
    Task<Editor?> GetByEmailAsync(string email);
}