using System.Data;
using Domain.Interfaces;
using Domain.Models;
using Npgsql;

namespace Infrastructure.Repositories.WithoutORM;

public class CustomerRepository : BaseRepository, ICustomerRepository
{
    public CustomerRepository(string connectionString) : base(connectionString) { }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        // Replace to procedure
        // const string query = "CALL add_project_asset(@projectId, @assetId)";

        const string query = "SELECT id, name FROM customers WHERE id = @id";

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);

        await using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new Customer
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1)
            };
        }

        return null;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        const string query = "SELECT id, name FROM customers";

        var customers = new List<Customer>();

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            customers.Add(new Customer
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1)
            });
        }

        return customers;
    }

    public async Task AddAsync(Customer entity)
    {
        const string procedure = "InsertCustomer"; // Хранимая процедура для обновления

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(procedure, connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("p_name", entity.Name);

        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateAsync(Customer entity)
    {
        const string procedure = "UpdateCustomer"; // Хранимая процедура для обновления

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(procedure, connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("p_name", entity.Name);
        command.Parameters.AddWithValue("p_id", entity.Id);

        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(Customer entity)
    {
        const string procedure = "DeleteCustomer"; // Хранимая процедура для обновления

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(procedure, connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("p_id", entity.Id);

        await command.ExecuteNonQueryAsync();
    }

    public async Task<Customer?> GetByNameAsync(string name)
    {
        const string query = "SELECT id, name FROM customers WHERE name = @name";

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@name", name);

        await using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new Customer
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1)
            };
        }

        return null;
    }
}