using Domain.Interfaces.LinkRepositories;
using Npgsql;

namespace Infrastructure.Repositories.WithoutORM.LinkRepositories;

public class ProjectDepartmentLinkRepository : BaseRepository, IProjectDepartmentLinkRepository
{
    public ProjectDepartmentLinkRepository(string connectionString) : base(connectionString) { }

    public async Task AddLinkAsync(int projectId, int departmentId)
    {
        const string query = "INSERT INTO project_department_links (project_id, department_id) VALUES (@projectId, @departmentId)";

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@projectId", projectId);
        command.Parameters.AddWithValue("@departmentId", departmentId);

        await command.ExecuteNonQueryAsync();
    }

    public async Task RemoveLinkAsync(int projectId, int departmentId)
    {
        const string query = "DELETE FROM project_department_links WHERE project_id = @projectId AND department_id = @departmentId";

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@projectId", projectId);
        command.Parameters.AddWithValue("@departmentId", departmentId);

        await command.ExecuteNonQueryAsync();
    }

    public async Task<IEnumerable<int>> GetDepartmentIdsByProjectIdAsync(int projectId)
    {
        const string query = "SELECT department_id FROM project_department_links WHERE project_id = @projectId";

        var departmentIds = new List<int>();

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@projectId", projectId);

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            departmentIds.Add(reader.GetInt32(0));
        }

        return departmentIds;
    }

    public async Task<IEnumerable<int>> GetProjectIdsByDepartmentIdAsync(int departmentId)
    {
        const string query = "SELECT project_id FROM project_department_links WHERE department_id = @departmentId";

        var projectIds = new List<int>();

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@departmentId", departmentId);

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            projectIds.Add(reader.GetInt32(0));
        }

        return projectIds;
    }
}
