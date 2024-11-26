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
            SELECT id, full_name, email, phone_number 
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
        const string query = "SELECT id, full_name, email, phone_number FROM editors";

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
        const string query = @"
            INSERT INTO editors (full_name, email, phone_number) 
            VALUES (@fullName, @email, @phoneNumber)";

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@fullName", entity.FullName);
        command.Parameters.AddWithValue("@email", entity.Email);
        command.Parameters.AddWithValue("@phoneNumber", entity.PhoneNumber);

        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateAsync(Editor entity)
    {
        const string query = @"
            UPDATE editors 
            SET full_name = @fullName, email = @email, phone_number = @phoneNumber 
            WHERE id = @id";

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@fullName", entity.FullName);
        command.Parameters.AddWithValue("@email", entity.Email);
        command.Parameters.AddWithValue("@phoneNumber", entity.PhoneNumber);
        command.Parameters.AddWithValue("@id", entity.Id);

        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(Editor entity)
    {
        const string query = "DELETE FROM editors WHERE id = @id";

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", entity.Id);

        await command.ExecuteNonQueryAsync();
    }

    public async Task<Editor?> GetByEmailAsync(string email)
    {
        const string query = @"
            SELECT id, full_name, email, phone_number 
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
