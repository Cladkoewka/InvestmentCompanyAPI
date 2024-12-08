using Npgsql;
using Application.DTOs.Functional;

namespace Infrastructure.Repositories.WithoutORM
{
    public class FunctionalRepository : BaseRepository
    {
        public FunctionalRepository(string connectionString) : base(connectionString)
        {
        }

        // Scalar Function
        public async Task<decimal> GetTotalProfitAsync()
        {
            const string query = "SELECT gettotalprofitminuscost()";

            await using var connection = await CreateConnectionAsync();
            await using var command = new NpgsqlCommand(query, connection);

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return reader.GetDecimal(0);
            }

            return 0m;
        }

        // Table Function
        public async Task<List<FunctionalProjectDto>> GetProjectsByCustomerNameAsync(string customerName)
        {
            const string query = "SELECT * FROM GetProjectsByCustomerName(@customerName)";

            var projects = new List<FunctionalProjectDto>();

            await using var connection = await CreateConnectionAsync();
            await using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@customerName", customerName);

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var project = new FunctionalProjectDto
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Status = reader.GetString(reader.GetOrdinal("Status")),
                    Profit = reader.GetDecimal(reader.GetOrdinal("Profit")),
                    Cost = reader.GetDecimal(reader.GetOrdinal("Cost")),
                    Deadline = reader.GetDateTime(reader.GetOrdinal("Deadline")),
                    CustomerName = reader.GetString(reader.GetOrdinal("CustomerName"))
                };

                projects.Add(project);
            }

            return projects;
        }
    }
}