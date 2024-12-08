using System.Data;
using Domain.Interfaces.LinkRepositories;
using Npgsql;

namespace Infrastructure.Repositories.WithoutORM.LinkRepositories;

public class ProjectAssetLinkRepository : BaseRepository, IProjectAssetLinkRepository
{
    public ProjectAssetLinkRepository(string connectionString) : base(connectionString) { }

    public async Task AddLinkAsync(int projectId, int assetId)
    {
        const string procedure = "InsertProjectAssetLink"; 

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(procedure, connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        
        
        command.Parameters.AddWithValue("p_projectid", projectId);
        command.Parameters.AddWithValue("p_assetid", assetId);
        
        await command.ExecuteNonQueryAsync();
    }

    public async Task RemoveLinkAsync(int projectId, int assetId)
    {
        const string procedure = "DeleteProjectAssetLink"; 

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(procedure, connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("p_projectid", projectId);
        command.Parameters.AddWithValue("p_assetid", assetId);

        await command.ExecuteNonQueryAsync();
    }

    public async Task<IEnumerable<int>> GetAssetIdsByProjectIdAsync(int projectId)
    {
        const string query = "SELECT assetid FROM ProjectAssetLinks WHERE projectid = @projectId";

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
        const string query = "SELECT projectid FROM ProjectAssetLinks WHERE assetid = @assetId";

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

    public async Task RemoveLinksByProjectIdAsync(int projectId)
    {
        const string query = "DELETE FROM ProjectAssetLinks WHERE projectid = @projectId";

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@projectId", projectId);

        await command.ExecuteNonQueryAsync();
    }

}