using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Common;
using Warehouse.Domain.Warehouses;
using Warehouse.Infrastructure.Common;

namespace Warehouse.Infrastructure.Products;
public class WarehouseRacksRepository : IWarehouseRacksRepository
{
    private readonly WarehouseDbContext _dbContext;

    public WarehouseRacksRepository(WarehouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddWarehouseRackAsync(WarehouseRack warehouseRack)
    {
        await _dbContext.WarehouseRacks.AddAsync(warehouseRack);
    }

    public async Task<WarehouseRack?> GetByIdAsync(Guid id)
    {
        return await _dbContext.WarehouseRacks.FindAsync(id);
    }

    public async Task<List<WarehouseRack>> ListAsync()
    {
        return await _dbContext.WarehouseRacks.ToListAsync();
    }

    public Task RemoveWarehouseRackAsync(WarehouseRack warehouseRack)
    {
        _dbContext.Remove(warehouseRack);

        return Task.CompletedTask;
    }

    public Task UpdateWarehouseRackAsync(WarehouseRack warehouseRack)
    {
        _dbContext.Update(warehouseRack);

        return Task.CompletedTask;
    }
}
