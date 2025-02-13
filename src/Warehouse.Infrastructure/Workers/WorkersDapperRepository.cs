using Dapper;
using Infrastructure.Persistence.Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Warehouse.Application.CommonDapper;
using Warehouse.Domain.Workers;

namespace Warehouse.Infrastructure.Workers;
public class WorkersDapperRepository : IWorkersDapperRepository
{
    private readonly IConfiguration configuration;

    public WorkersDapperRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public async Task AddWorkerAsync(Worker worker)
    {
        using (var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var query = "INSERT INTO Workers (Id, FirstName, LastName, Role) VALUES (@id, @firstName, @lastName, @role)";
            
            await connection.ExecuteAsync(query, new
            {
                id = worker.Id.ToString(),
                firstName = worker.FirstName,
                lastName = worker.LastName,
                role = worker.Role.ToString()
            });
        }
    }

    public async Task<Worker?> GetByIdAsync(Guid id)
    {
        using (var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            SqlMapper.AddTypeHandler(new GuidTypeHandler());
            var query = "SELECT * FROM Workers WHERE Id = @id";
            return await connection.QueryFirstOrDefaultAsync<Worker>(query, new { id = id.ToString() });
        }
    }

    public async Task<List<Worker>> ListAsync()
    {
        using (var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            SqlMapper.AddTypeHandler(new GuidTypeHandler());
            var query = "SELECT * FROM Workers";
            var workers = await connection.QueryAsync<Worker>(query);
            return workers.ToList();
        }
    }

    public Task RemoveWorkerAsync(Worker worker)
    {
        using (var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var query = "DELETE FROM Workers WHERE Id = @id";
            connection.Execute(query, new { id = worker.Id.ToString() });
            return Task.CompletedTask;
        }
    }

    public Task UpdateWorkerAsync(Worker worker)
    {
        using (var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var query = "UPDATE Workers SET FirstName = @firstName, LastName = @lastName, Role = @role WHERE Id = @id";
            
            connection.ExecuteAsync(query, new
            {
                id = worker.Id.ToString(),
                firstName = worker.FirstName,
                lastName = worker.LastName,
                role = worker.Role
            });
            return Task.CompletedTask;
        }
    }
}
