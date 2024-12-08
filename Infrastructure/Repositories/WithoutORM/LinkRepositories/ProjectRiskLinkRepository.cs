using System.Data;
using Domain.Interfaces.LinkRepositories;
using Npgsql;

namespace Infrastructure.Repositories.WithoutORM.LinkRepositories;

public class ProjectRiskLinkRepository : BaseRepository, IProjectRiskLinkRepository
{
    public ProjectRiskLinkRepository(string connectionString) : base(connectionString) { }

    public async Task AddLinkAsync(int projectId, int riskId)
    {
        const string procedure = "InsertProjectRiskLink"; 

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(procedure, connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("p_projectid", projectId);
        command.Parameters.AddWithValue("p_riskid", riskId);

        await command.ExecuteNonQueryAsync();
    }

    public async Task RemoveLinkAsync(int projectId, int riskId)
    {
        const string procedure = "DeleteProjectRiskLink"; 

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(procedure, connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("p_projectid", projectId);
        command.Parameters.AddWithValue("p_riskid", riskId);

        await command.ExecuteNonQueryAsync();
    }


    public async Task<IEnumerable<int>> GetRiskIdsByProjectIdAsync(int projectId)
    {
        const string query = "SELECT riskid FROM projectrisklinks WHERE projectid = @projectId";

        var riskIds = new List<int>();

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@projectId", projectId);

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            riskIds.Add(reader.GetInt32(0));
        }

        return riskIds;
    }

    public async Task<IEnumerable<int>> GetProjectIdsByRiskIdAsync(int riskId)
    {
        const string query = "SELECT projectid FROM projectrisklinks WHERE riskid = @riskId";

        var projectIds = new List<int>();

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@riskId", riskId);

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            projectIds.Add(reader.GetInt32(0));
        }

        return projectIds;
    }

    public async Task RemoveRisksByProjectIdAsync(int projectId)
    {
        const string query = "DELETE FROM ProjectRiskLinks WHERE projectid = @projectId";

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@projectId", projectId);

        await command.ExecuteNonQueryAsync();
    }
}
