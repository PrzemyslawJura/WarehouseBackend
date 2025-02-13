using Dapper;
using Infrastructure.Persistence.Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Warehouse.Application.CommonDapper;
using Warehouse.Domain.WarehousesSize;

namespace Warehouse.Infrastructure.WarehousesSize;
public class WarehousesSizeDapperRepository : IWarehousesSizeDapperRepository
{
    private readonly IConfiguration configuration;

    public WarehousesSizeDapperRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public async Task AddWarehouseSizeAsync(WarehouseSize warehouseSize)
    {
        using (var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var query = "INSERT INTO WarehousesSize (Id, Name, SectorNumber, RackQuantity) VALUES (@id, @name, @sectorNumber, @rackQuantity)";
            
            await connection.ExecuteAsync(query, new
            {
                id = warehouseSize.Id.ToString(),
                name = warehouseSize.Name,
                sectorNumber = warehouseSize.SectorNumber,
                rackQuantity = warehouseSize.RackQuantity
            });
        }
    }

    public async Task<WarehouseSize?> GetByIdAsync(Guid id)
    {
        using (var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            SqlMapper.AddTypeHandler(new GuidTypeHandler());
            var query = "SELECT * FROM WarehousesSize WHERE Id = @id";
            return await connection.QueryFirstOrDefaultAsync<WarehouseSize>(query, new { id = id.ToString() });
        }
    }

    public async Task<List<WarehouseSize>> ListAsync()
    {
        using (var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            SqlMapper.AddTypeHandler(new GuidTypeHandler());
            var query = "SELECT * FROM WarehousesSize";
            var warehousesSize = await connection.QueryAsync<WarehouseSize>(query);
            return warehousesSize.ToList();
        }
    }

    public Task RemoveWarehouseSizeAsync(WarehouseSize warehouseSize)
    {
        using (var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var query = "DELETE FROM WarehousesSize WHERE Id = @id";
            connection.Execute(query, new { id = warehouseSize.Id.ToString() });
            return Task.CompletedTask;
        }
    }

    public Task UpdateWarehouseSizeAsync(WarehouseSize warehouseSize)
    {
        using (var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var query = "UPDATE WarehousesSize SET Name = @name, SectorNumber = @sectorNumber, RackQuantity = @rackQuantity WHERE Id = @id";
            
            connection.ExecuteAsync(query, new
            {
                id = warehouseSize.Id.ToString(),
                name = warehouseSize.Name,
                sectorNumber = warehouseSize.SectorNumber,
                rackQuantity = warehouseSize.RackQuantity
            });
            return Task.CompletedTask;
        }
    }
}
