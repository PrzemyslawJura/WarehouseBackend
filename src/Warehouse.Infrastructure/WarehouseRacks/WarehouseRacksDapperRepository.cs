using Dapper;
using Infrastructure.Persistence.Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Warehouse.Application.CommonDapper;
using Warehouse.Domain.Warehouses;

namespace Warehouse.Infrastructure.WarehouseRacks;
public class WarehouseRacksDapperRepository : IWarehouseRacksDapperRepository
{
    private readonly IConfiguration configuration;

    public WarehouseRacksDapperRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public async Task AddWarehouseRackAsync(WarehouseRack warehouseRack)
    {
        using (var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var query = "INSERT INTO WarehouseRacks (Id, Sector, Rack, Quantity, WarehouseSizeId) VALUES (@id, @sector, @rack, @quantity, @warehouseSizeId)";
            
            await connection.ExecuteAsync(query, new
            {
                id = warehouseRack.Id.ToString(),
                sector = warehouseRack.Sector,
                rack = warehouseRack.Rack,
                quantity = warehouseRack.Quantity,
                warehouseSizeId = warehouseRack.WarehouseSizeId.ToString()
            });
        }
    }

    public async Task<WarehouseRack?> GetByIdAsync(Guid id)
    {
        using (var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            SqlMapper.AddTypeHandler(new GuidTypeHandler());
            var query = "SELECT * FROM WarehouseRacks WHERE Id = @id";
            return await connection.QueryFirstOrDefaultAsync<WarehouseRack>(query, new { id = id.ToString() });
        }
    }

    public async Task<List<WarehouseRack>> ListAsync()
    {
        using (var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            SqlMapper.AddTypeHandler(new GuidTypeHandler());
            var query = "SELECT * FROM WarehouseRacks";
            var warehouseRack = await connection.QueryAsync<WarehouseRack>(query);
            return warehouseRack.ToList();
        }
    }

    public Task RemoveWarehouseRackAsync(WarehouseRack warehouseRack)
    {
        using (var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var query = "DELETE FROM  WarehouseRacks WHERE Id = @id";
            connection.Execute(query, new { id =  warehouseRack.Id.ToString() });
            return Task.CompletedTask;
        }
    }

    public Task UpdateWarehouseRackAsync(WarehouseRack warehouseRack)
    {
        using (var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var query = "UPDATE WarehouseRacks SET Sector = @sector, Rack = @rack, Quantity = @quantity, WarehouseSizeId = @warehouseSizeId WHERE Id = @id";
            
            connection.ExecuteAsync(query, new
            {
                id = warehouseRack.Id.ToString(),
                sector = warehouseRack.Sector,
                rack = warehouseRack.Rack,
                quantity = warehouseRack.Quantity,
                warehouseSizeId = warehouseRack.WarehouseSizeId.ToString()
            });
            return Task.CompletedTask;
        }
    }
}
