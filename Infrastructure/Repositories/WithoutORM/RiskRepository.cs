using System.Data;
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
        const string procedure = "InsertRisk"; 

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(procedure, connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("p_type", entity.Type);
        command.Parameters.AddWithValue("p_grade", entity.Grade);

        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateAsync(Risk entity)
    {
        const string procedure = "UpdateRisk"; 

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(procedure, connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("p_id", entity.Id);
        command.Parameters.AddWithValue("p_type", entity.Type);
        command.Parameters.AddWithValue("p_grade", entity.Grade);

        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(Risk entity)
    {
        const string procedure = "DeleteRisk"; 

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(procedure, connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("p_id", entity.Id);

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
