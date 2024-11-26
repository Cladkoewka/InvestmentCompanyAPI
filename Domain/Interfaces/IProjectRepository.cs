using Domain.Models;

namespace Domain.Interfaces;

public interface IProjectRepository : IRepository<Project>
{
    Task<IEnumerable<Project>> GetByCustomerIdAsync(int customerId);
    Task<IEnumerable<Project>> GetByEditorIdAsync(int editorId);
}