using System.Data;
using Domain.Interfaces;
using Domain.Models;
using Npgsql;

namespace Infrastructure.Repositories.WithoutORM;

public class AssetRepository : BaseRepository, IAssetRepository
{
    public AssetRepository(string connectionString) : base(connectionString)
    {
    }

    public async Task<Asset?> GetByIdAsync(int id)
    {
        const string query = "SELECT id, name FROM assets WHERE id = @id";

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);

        await using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new Asset
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1)
            };
        }

        return null;
    }

    public async Task<IEnumerable<Asset>> GetAllAsync()
    {
        const string query = "SELECT id, name FROM assets";

        var assets = new List<Asset>();

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            assets.Add(new Asset
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1)
            });
        }

        return assets;
    }

    public async Task AddAsync(Asset entity)
    {
        const string procedure = "InsertAsset"; // Имя процедуры

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(procedure, connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("p_name", entity.Name);

        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateAsync(Asset entity)
    {
        const string procedure = "UpdateAsset"; // Хранимая процедура для обновления

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(procedure, connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("p_name", entity.Name);
        command.Parameters.AddWithValue("p_id", entity.Id);

        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(Asset entity)
    {
        const string procedure = "DeleteAsset"; // Хранимая процедура для удаления

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(procedure, connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("p_id", entity.Id);

        await command.ExecuteNonQueryAsync();
    }

    public async Task<IEnumerable<Asset>> GetByNameAsync(string name)
    {
        const string query = "SELECT id, name FROM assets WHERE name = @name";

        var assets = new List<Asset>();

        await using var connection = await CreateConnectionAsync();
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("name", name);

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            assets.Add(new Asset
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1)
            });
        }

        return assets;
    }
}