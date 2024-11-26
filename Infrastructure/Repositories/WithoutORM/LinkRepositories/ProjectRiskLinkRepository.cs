using Domain.Interfaces.LinkRepositories;
using Npgsql;

namespace Infrastructure.Repositories.WithoutORM.LinkRepositories;

public class ProjectRiskLinkRepository : BaseRepository, IProjectRiskLinkRepository
{
    public ProjectRiskLinkRepository(string connectionString) : base(connectionString) { }

    public async Task AddLinkAsync(int projectId, int riskId)
    {
        const string query = "INSERT INTO project_risk_links (project_id, risk_id) VALUES (@projectId, @riskId)";

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@projectId", projectId);
        command.Parameters.AddWithValue("@riskId", riskId);

        await command.ExecuteNonQueryAsync();
    }

    public async Task RemoveLinkAsync(int projectId, int riskId)
    {
        const string query = "DELETE FROM project_risk_links WHERE project_id = @projectId AND risk_id = @riskId";

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@projectId", projectId);
        command.Parameters.AddWithValue("@riskId", riskId);

        await command.ExecuteNonQueryAsync();
    }

    public async Task<IEnumerable<int>> GetRiskIdsByProjectIdAsync(int projectId)
    {
        const string query = "SELECT risk_id FROM project_risk_links WHERE project_id = @projectId";

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
        const string query = "SELECT project_id FROM project_risk_links WHERE risk_id = @riskId";

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
}
