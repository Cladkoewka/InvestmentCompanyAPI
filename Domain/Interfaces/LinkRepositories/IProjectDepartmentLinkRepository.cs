namespace Domain.Interfaces.LinkRepositories;

public interface IProjectDepartmentLinkRepository
{
    Task AddLinkAsync(int projectId, int departmentId);                  
    Task RemoveLinkAsync(int projectId, int departmentId);               
    Task<IEnumerable<int>> GetDepartmentIdsByProjectIdAsync(int projectId); 
    Task<IEnumerable<int>> GetProjectIdsByDepartmentIdAsync(int departmentId);
}