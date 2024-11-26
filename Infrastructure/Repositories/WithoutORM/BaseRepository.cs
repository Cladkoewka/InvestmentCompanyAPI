using Npgsql;

namespace Infrastructure.Repositories.WithoutORM;

public abstract class BaseRepository
{
    private readonly string _connectionString;

    public BaseRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected async Task<NpgsqlConnection> CreateConnectionAsync()
    {
        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        return connection;
    }
}