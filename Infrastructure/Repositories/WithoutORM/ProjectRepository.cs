using Domain.Interfaces;
using Domain.Models;
using Npgsql;

namespace Infrastructure.Repositories.WithoutORM;

public class ProjectRepository : BaseRepository, IProjectRepository
{
    public ProjectRepository(string connectionString) : base(connectionString) { }

    public async Task<Project?> GetByIdAsync(int id)
    {
        const string query = "SELECT id, name, status, profit, cost, deadline, customer_id, editor_id FROM projects WHERE id = @id";

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);

        await using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new Project
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Status = reader.GetString(2),
                Profit = reader.GetDecimal(3),
                Cost = reader.GetDecimal(4),
                Deadline = reader.GetDateTime(5),
                CustomerId = reader.GetInt32(6),
                EditorId = reader.GetInt32(7)
            };
        }

        return null;
    }

    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        const string query = "SELECT id, name, status, profit, cost, deadline, customer_id, editor_id FROM projects";

        var projects = new List<Project>();

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            projects.Add(new Project
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Status = reader.GetString(2),
                Profit = reader.GetDecimal(3),
                Cost = reader.GetDecimal(4),
                Deadline = reader.GetDateTime(5),
                CustomerId = reader.GetInt32(6),
                EditorId = reader.GetInt32(7)
            });
        }

        return projects;
    }

    public async Task AddAsync(Project entity)
    {
        const string query = @"
            INSERT INTO projects (name, status, profit, cost, deadline, customer_id, editor_id) 
            VALUES (@name, @status, @profit, @cost, @deadline, @customerId, @editorId)";

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@name", entity.Name);
        command.Parameters.AddWithValue("@status", entity.Status);
        command.Parameters.AddWithValue("@profit", entity.Profit);
        command.Parameters.AddWithValue("@cost", entity.Cost);
        command.Parameters.AddWithValue("@deadline", entity.Deadline);
        command.Parameters.AddWithValue("@customerId", entity.CustomerId);
        command.Parameters.AddWithValue("@editorId", entity.EditorId);

        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateAsync(Project entity)
    {
        const string query = @"
            UPDATE projects 
            SET name = @name, status = @status, profit = @profit, cost = @cost, deadline = @deadline, 
                customer_id = @customerId, editor_id = @editorId
            WHERE id = @id";

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", entity.Id);
        command.Parameters.AddWithValue("@name", entity.Name);
        command.Parameters.AddWithValue("@status", entity.Status);
        command.Parameters.AddWithValue("@profit", entity.Profit);
        command.Parameters.AddWithValue("@cost", entity.Cost);
        command.Parameters.AddWithValue("@deadline", entity.Deadline);
        command.Parameters.AddWithValue("@customerId", entity.CustomerId);
        command.Parameters.AddWithValue("@editorId", entity.EditorId);

        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(Project entity)
    {
        const string query = "DELETE FROM projects WHERE id = @id";

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", entity.Id);

        await command.ExecuteNonQueryAsync();
    }

    public async Task<IEnumerable<Project>> GetByCustomerIdAsync(int customerId)
    {
        const string query = @"
            SELECT id, name, status, profit, cost, deadline, customer_id, editor_id 
            FROM projects 
            WHERE customer_id = @customerId";

        var projects = new List<Project>();

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@customerId", customerId);

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            projects.Add(new Project
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Status = reader.GetString(2),
                Profit = reader.GetDecimal(3),
                Cost = reader.GetDecimal(4),
                Deadline = reader.GetDateTime(5),
                CustomerId = reader.GetInt32(6),
                EditorId = reader.GetInt32(7)
            });
        }

        return projects;
    }

    public async Task<IEnumerable<Project>> GetByEditorIdAsync(int editorId)
    {
        const string query = @"
            SELECT id, name, status, profit, cost, deadline, customer_id, editor_id 
            FROM projects 
            WHERE editor_id = @editorId";

        var projects = new List<Project>();

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@editorId", editorId);

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            projects.Add(new Project
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Status = reader.GetString(2),
                Profit = reader.GetDecimal(3),
                Cost = reader.GetDecimal(4),
                Deadline = reader.GetDateTime(5),
                CustomerId = reader.GetInt32(6),
                EditorId = reader.GetInt32(7)
            });
        }

        return projects;
    }
}
