using System.Data;
using Domain.Interfaces;
using Domain.Models;
using Npgsql;

namespace Infrastructure.Repositories.WithoutORM;

public class EditorRepository : BaseRepository, IEditorRepository
{
    public EditorRepository(string connectionString) : base(connectionString) { }

    public async Task<Editor?> GetByIdAsync(int id)
    {
        const string query = @"
            SELECT id, fullname, email, phonenumber 
            FROM editors 
            WHERE id = @id";

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);

        await using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new Editor
            {
                Id = reader.GetInt32(0),
                FullName = reader.GetString(1),
                Email = reader.GetString(2),
                PhoneNumber = reader.GetString(3)
            };
        }

        return null;
    }

    public async Task<IEnumerable<Editor>> GetAllAsync()
    {
        const string query = "SELECT id, fullname, email, phonenumber FROM editors";

        var editors = new List<Editor>();

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            editors.Add(new Editor
            {
                Id = reader.GetInt32(0),
                FullName = reader.GetString(1),
                Email = reader.GetString(2),
                PhoneNumber = reader.GetString(3)
            });
        }

        return editors;
    }

    public async Task AddAsync(Editor entity)
    {
        const string function = "InsertEditor"; // Имя функции

        await using var connection = await CreateConnectionAsync();
        var query = $"SELECT {function}(@p_fullname, @p_email, @p_phonenumber)";

        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("p_fullname", entity.FullName);
        command.Parameters.AddWithValue("p_email", entity.Email);
        command.Parameters.AddWithValue("p_phonenumber", entity.PhoneNumber);

        var result = await command.ExecuteScalarAsync();
        entity.Id = Convert.ToInt32(result);
    }


    public async Task UpdateAsync(Editor entity)
    {
        const string procedure = "UpdateEditor"; 

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(procedure, connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("p_fullname", entity.FullName);
        command.Parameters.AddWithValue("p_email", entity.Email);
        command.Parameters.AddWithValue("p_phonenumber", entity.PhoneNumber);
        command.Parameters.AddWithValue("p_id", entity.Id);

        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(Editor entity)
    {
        const string procedure = "DeleteEditor"; 

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(procedure, connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("p_id", entity.Id);

        await command.ExecuteNonQueryAsync();
    }

    public async Task<Editor?> GetByEmailAsync(string email)
    {
        const string query = @"
            SELECT id, fullname, email, phonenumber 
            FROM editors 
            WHERE email = @email";

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@email", email);

        await using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new Editor
            {
                Id = reader.GetInt32(0),
                FullName = reader.GetString(1),
                Email = reader.GetString(2),
                PhoneNumber = reader.GetString(3)
            };
        }

        return null;
    }
}
