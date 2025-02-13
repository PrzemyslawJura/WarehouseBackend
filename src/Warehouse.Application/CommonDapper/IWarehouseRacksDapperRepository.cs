using Warehouse.Domain.Warehouses;

namespace Warehouse.Application.CommonDapper;
public interface IWarehouseRacksDapperRepository
{
    Task AddWarehouseRackAsync(WarehouseRack warehouseRack);
    Task<WarehouseRack?> GetByIdAsync(Guid id);
    Task<List<WarehouseRack>> ListAsync();
    Task UpdateWarehouseRackAsync(WarehouseRack warehouseRack);
    Task RemoveWarehouseRackAsync(WarehouseRack warehouseRack);
}
