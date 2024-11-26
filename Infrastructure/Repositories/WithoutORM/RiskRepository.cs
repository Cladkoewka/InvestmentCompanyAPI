using Domain.Interfaces;
using Domain.Models;
using Npgsql;

namespace Infrastructure.Repositories.WithoutORM;

public class RiskRepository : BaseRepository, IRiskRepository
{
    public RiskRepository(string connectionString) : base(connectionString) { }

    public async Task<Risk?> GetByIdAsync(int id)
    {
        const string query = "SELECT id, type, grade FROM risks WHERE id = @id";

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);

        await using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new Risk
            {
                Id = reader.GetInt32(0),
                Type = reader.GetString(1),
                Grade = reader.GetInt32(2)
            };
        }

        return null;
    }

    public async Task<IEnumerable<Risk>> GetAllAsync()
    {
        const string query = "SELECT id, type, grade FROM risks";

        var risks = new List<Risk>();

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            risks.Add(new Risk
            {
                Id = reader.GetInt32(0),
                Type = reader.GetString(1),
                Grade = reader.GetInt32(2)
            });
        }

        return risks;
    }

    public async Task AddAsync(Risk entity)
    {
        const string query = @"
            INSERT INTO risks (type, grade) 
            VALUES (@type, @grade)";

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@type", entity.Type);
        command.Parameters.AddWithValue("@grade", entity.Grade);

        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateAsync(Risk entity)
    {
        const string query = @"
            UPDATE risks 
            SET type = @type, grade = @grade 
            WHERE id = @id";

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", entity.Id);
        command.Parameters.AddWithValue("@type", entity.Type);
        command.Parameters.AddWithValue("@grade", entity.Grade);

        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(Risk entity)
    {
        const string query = "DELETE FROM risks WHERE id = @id";

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", entity.Id);

        await command.ExecuteNonQueryAsync();
    }

    public async Task<IEnumerable<Risk>> GetByGradeAsync(int grade)
    {
        const string query = "SELECT id, type, grade FROM risks WHERE grade = @grade";

        var risks = new List<Risk>();

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@grade", grade);

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            risks.Add(new Risk
            {
                Id = reader.GetInt32(0),
                Type = reader.GetString(1),
                Grade = reader.GetInt32(2)
            });
        }

        return risks;
    }
}
