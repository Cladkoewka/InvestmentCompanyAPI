using System.Data;
using Domain.Interfaces;
using Domain.Models;
using Npgsql;

namespace Infrastructure.Repositories.WithoutORM;

public class EmployeeRepository : BaseRepository, IEmployeeRepository
{
    public EmployeeRepository(string connectionString) : base(connectionString) { }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        const string query = "SELECT id, firstname, lastname, departmentid FROM employees WHERE id = @id";

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);

        await using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new Employee
            {
                Id = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                DepartmentId = reader.GetInt32(3)
            };
        }

        return null;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        const string query = "SELECT id, firstname, lastname, departmentid FROM employees";

        var employees = new List<Employee>();

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            employees.Add(new Employee
            {
                Id = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                DepartmentId = reader.GetInt32(3)
            });
        }

        return employees;
    }

    public async Task AddAsync(Employee entity)
    {
        const string function = "InsertEmployee"; // Имя функции

        await using var connection = await CreateConnectionAsync();
        var query = $"SELECT {function}(@p_firstname, @p_lastname, @p_departmentid)";

        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("p_firstname", entity.FirstName);
        command.Parameters.AddWithValue("p_lastname", entity.LastName);
        command.Parameters.AddWithValue("p_departmentid", entity.DepartmentId);

        var result = await command.ExecuteScalarAsync();
        entity.Id = Convert.ToInt32(result);
    }


    public async Task UpdateAsync(Employee entity)
    {
        const string procedure = "UpdateEmployee"; 

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(procedure, connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("p_firstname", entity.FirstName);
        command.Parameters.AddWithValue("p_lastname", entity.LastName);
        command.Parameters.AddWithValue("p_departmentid", entity.DepartmentId);
        command.Parameters.AddWithValue("p_id", entity.Id);

        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(Employee entity)
    {
        const string procedure = "DeleteEmployee"; 

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(procedure, connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("p_id", entity.Id);

        await command.ExecuteNonQueryAsync();
    }

    public async Task<IEnumerable<Employee>> GetByDepartmentIdAsync(int departmentId)
    {
        const string query = "SELECT id, firstname, lastname, departmentid FROM employees WHERE departmentid = @departmentId";

        var employees = new List<Employee>();
        
        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@departmentId", departmentId);

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            employees.Add(new Employee
            {
                Id = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                DepartmentId = reader.GetInt32(3)
            });
        }

        return employees;
    }
}
