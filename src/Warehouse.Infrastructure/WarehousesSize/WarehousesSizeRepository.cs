using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Common;
using Warehouse.Domain.WarehousesSize;
using Warehouse.Infrastructure.Common;

namespace Warehouse.Infrastructure.Products;
public class WarehousesSizeRepository : IWarehousesSizeRepository
{
    private readonly WarehouseDbContext _dbContext;

    public WarehousesSizeRepository(WarehouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddWarehouseSizeAsync(WarehouseSize warehouseSize)
    {
        await _dbContext.WarehousesSize.AddAsync(warehouseSize);
    }

    public async Task<WarehouseSize?> GetByIdAsync(Guid id)
    {
        return await _dbContext.WarehousesSize.FindAsync(id);
    }

    public async Task<List<WarehouseSize>> ListAsync()
    {
        return await _dbContext.WarehousesSize.ToListAsync();
    }

    public Task RemoveWarehouseSizeAsync(WarehouseSize warehouseSize)
    {
        _dbContext.Remove(warehouseSize);

        return Task.CompletedTask;
    }

    public Task UpdateWarehouseSizeAsync(WarehouseSize warehouseSize)
    {
        _dbContext.Update(warehouseSize);

        return Task.CompletedTask;
    }
}
