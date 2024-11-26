using Domain.Interfaces;
using Domain.Models;
using Npgsql;

namespace Infrastructure.Repositories.WithoutORM;

public class EmployeeRepository : BaseRepository, IEmployeeRepository
{
    public EmployeeRepository(string connectionString) : base(connectionString) { }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        const string query = "SELECT id, first_name, last_name, department_id FROM employees WHERE id = @id";

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
        const string query = "SELECT id, first_name, last_name, department_id FROM employees";

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
        const string query = @"
            INSERT INTO employees (first_name, last_name, department_id) 
            VALUES (@firstName, @lastName, @departmentId)";

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@firstName", entity.FirstName);
        command.Parameters.AddWithValue("@lastName", entity.LastName);
        command.Parameters.AddWithValue("@departmentId", entity.DepartmentId);

        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateAsync(Employee entity)
    {
        const string query = @"
            UPDATE employees 
            SET first_name = @firstName, last_name = @lastName, department_id = @departmentId 
            WHERE id = @id";

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@firstName", entity.FirstName);
        command.Parameters.AddWithValue("@lastName", entity.LastName);
        command.Parameters.AddWithValue("@departmentId", entity.DepartmentId);
        command.Parameters.AddWithValue("@id", entity.Id);

        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(Employee entity)
    {
        const string query = "DELETE FROM employees WHERE id = @id";

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", entity.Id);

        await command.ExecuteNonQueryAsync();
    }

    public async Task<IEnumerable<Employee>> GetByDepartmentIdAsync(int departmentId)
    {
        const string query = "SELECT id, first_name, last_name, department_id FROM employees WHERE department_id = @departmentId";

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
