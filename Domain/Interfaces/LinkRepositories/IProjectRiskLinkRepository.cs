namespace Domain.Interfaces.LinkRepositories;

public interface IProjectRiskLinkRepository
{
    Task AddLinkAsync(int projectId, int riskId);                  
    Task RemoveLinkAsync(int projectId, int riskId);               
    Task<IEnumerable<int>> GetRiskIdsByProjectIdAsync(int projectId); 
    Task<IEnumerable<int>> GetProjectIdsByRiskIdAsync(int riskId);
    Task RemoveRisksByProjectIdAsync(int projectId);
}