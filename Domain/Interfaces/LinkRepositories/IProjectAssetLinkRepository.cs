namespace Domain.Interfaces.LinkRepositories;

public interface IProjectAssetLinkRepository
{
    Task AddLinkAsync(int projectId, int assetId);                  
    Task RemoveLinkAsync(int projectId, int assetId);               
    Task<IEnumerable<int>> GetAssetIdsByProjectIdAsync(int projectId);
    Task<IEnumerable<int>> GetProjectIdsByAssetIdAsync(int assetId);
}