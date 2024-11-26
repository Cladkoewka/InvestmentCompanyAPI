using Domain.Interfaces.LinkRepositories;
using Npgsql;

namespace Infrastructure.Repositories.WithoutORM.LinkRepositories;

public class ProjectAssetLinkRepository : BaseRepository, IProjectAssetLinkRepository
{
    public ProjectAssetLinkRepository(string connectionString) : base(connectionString) { }

    public async Task AddLinkAsync(int projectId, int assetId)
    {
        const string query = "INSERT INTO project_assets (project_id, asset_id) VALUES (@projectId, @assetId)";

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@projectId", projectId);
        command.Parameters.AddWithValue("@assetId", assetId);

        await command.ExecuteNonQueryAsync();
    }

    public async Task RemoveLinkAsync(int projectId, int assetId)
    {
        const string query = "DELETE FROM project_assets WHERE project_id = @projectId AND asset_id = @assetId";

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@projectId", projectId);
        command.Parameters.AddWithValue("@assetId", assetId);

        await command.ExecuteNonQueryAsync();
    }

    public async Task<IEnumerable<int>> GetAssetIdsByProjectIdAsync(int projectId)
    {
        const string query = "SELECT asset_id FROM project_assets WHERE project_id = @projectId";

        var assetIds = new List<int>();

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@projectId", projectId);

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            assetIds.Add(reader.GetInt32(0));
        }

        return assetIds;
    }

    public async Task<IEnumerable<int>> GetProjectIdsByAssetIdAsync(int assetId)
    {
        const string query = "SELECT project_id FROM project_assets WHERE asset_id = @assetId";

        var projectIds = new List<int>();

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@assetId", assetId);

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            projectIds.Add(reader.GetInt32(0));
        }

        return projectIds;
    }
}