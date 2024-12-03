using System.Data;
using Domain.Interfaces;
using Domain.Models;
using Npgsql;

namespace Infrastructure.Repositories.WithoutORM;

public class DepartmentRepository : BaseRepository, IDepartmentRepository
{
    public DepartmentRepository(string connectionString) : base(connectionString) { }

    public async Task<IEnumerable<Department>> GetAllAsync()
    {
        const string query = "SELECT id, name FROM departments";

        var departments = new List<Department>();

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            departments.Add(new Department
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1)
            });
        }

        return departments;
    }

    public async Task<Department?> GetByIdAsync(int id)
    {
        const string query = "SELECT id, name FROM departments WHERE id = @id";

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);

        await using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new Department
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1)
            };
        }

        return null;
    }

    public async Task<Department?> GetByNameAsync(string name)
    {
        const string query = "SELECT id, name FROM departments WHERE name = @name";

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@name", name);

        await using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new Department
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1)
            };
        }

        return null;
    }

    public async Task AddAsync(Department entity)
    {
        const string function = "InsertDepartment"; 

        await using var connection = await CreateConnectionAsync();
        var query = $"SELECT {function}(@p_name)";

        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("p_name", entity.Name);

        var result = await command.ExecuteScalarAsync();
        entity.Id = Convert.ToInt32(result);
    }

    public async Task UpdateAsync(Department entity)
    {
        const string procedure = "UpdateDepartment"; 

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(procedure, connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("p_name", entity.Name);
        command.Parameters.AddWithValue("p_id", entity.Id);

        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(Department entity)
    {
        const string procedure = "DeleteDepartment"; 

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(procedure, connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("p_id", entity.Id);

        await command.ExecuteNonQueryAsync();
    }
}
