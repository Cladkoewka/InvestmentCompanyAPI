using System.Data;
using Domain.Interfaces;
using Domain.Models;
using Npgsql;

namespace Infrastructure.Repositories.WithoutORM;

public class ProjectRepository : BaseRepository, IProjectRepository
{
    public ProjectRepository(string connectionString) : base(connectionString) { }
    
    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        const string query = "SELECT id, name, status, profit, cost, deadline, customerid, editorid FROM projects";

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

    public async Task<Project?> GetByIdAsync(int id)
    {
        const string query = "SELECT id, name, status, profit, cost, deadline, customerid, editorid FROM projects WHERE id = @id";

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
    
    public async Task<IEnumerable<Project>> GetByCustomerIdAsync(int customerId)
    {
        const string query = @"
            SELECT id, name, status, profit, cost, deadline, customerid, editorid 
            FROM projects 
            WHERE customerid = @customerId";

        var projects = new List<Project>();

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@customerid", customerId);

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
            SELECT id, name, status, profit, cost, deadline, customerid, editorid 
            FROM projects 
            WHERE editorid = @editorId";

        var projects = new List<Project>();

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@editorid", editorId);

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
        const string function = "InsertProject"; // Имя функции

        await using var connection = await CreateConnectionAsync();
        var query = $"SELECT {function}(@p_name, @p_status, @p_profit, @p_cost, @p_deadline, @p_customerid, @p_editorid)";

        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("p_name", entity.Name);
        command.Parameters.AddWithValue("p_status", entity.Status);
        command.Parameters.AddWithValue("p_profit", entity.Profit);
        command.Parameters.AddWithValue("p_cost", entity.Cost);
        command.Parameters.AddWithValue("p_deadline", entity.Deadline);
        command.Parameters.AddWithValue("p_customerid", entity.CustomerId);
        command.Parameters.AddWithValue("p_editorid", entity.EditorId);

        var result = await command.ExecuteScalarAsync();
        entity.Id = Convert.ToInt32(result);
    }


    public async Task UpdateAsync(Project entity)
    {
        const string procedure = "UpdateProject"; 

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(procedure, connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("p_id", entity.Id);
        command.Parameters.AddWithValue("p_name", entity.Name);
        command.Parameters.AddWithValue("p_status", entity.Status);
        command.Parameters.AddWithValue("p_profit", entity.Profit);
        command.Parameters.AddWithValue("p_cost", entity.Cost);
        command.Parameters.AddWithValue("p_deadline", entity.Deadline);
        command.Parameters.AddWithValue("p_customerid", entity.CustomerId);
        command.Parameters.AddWithValue("p_editorid", entity.EditorId);

        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(Project entity)
    {
        const string procedure = "DeleteProject"; 

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(procedure, connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("p_id", entity.Id);

        await command.ExecuteNonQueryAsync();
    }

    
}
