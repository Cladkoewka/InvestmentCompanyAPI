using System.Data;
using Domain.Interfaces.LinkRepositories;
using Npgsql;

namespace Infrastructure.Repositories.WithoutORM.LinkRepositories;

public class ProjectDepartmentLinkRepository : BaseRepository, IProjectDepartmentLinkRepository
{
    public ProjectDepartmentLinkRepository(string connectionString) : base(connectionString) { }

    public async Task AddLinkAsync(int projectId, int departmentId)
    {
        const string procedure = "InsertProjectDepartmentLink"; 

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(procedure, connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("p_projectid", projectId);
        command.Parameters.AddWithValue("p_departmentid", departmentId);

        await command.ExecuteNonQueryAsync();
    }

    public async Task RemoveLinkAsync(int projectId, int departmentId)
    {
        const string procedure = "DeleteProjectDepartmentLink"; 

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(procedure, connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("p_projectid", projectId);
        command.Parameters.AddWithValue("p_departmentid", departmentId);

        await command.ExecuteNonQueryAsync();
    }

    public async Task<IEnumerable<int>> GetDepartmentIdsByProjectIdAsync(int projectId)
    {
        const string query = "SELECT departmentid FROM projectdepartmentlinks WHERE projectid = @projectId";

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
        const string query = "SELECT projectid FROM projectdepartmentlinks WHERE departmentid = @departmentId";

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
