using Warehouse.Domain.WarehousesSize;

namespace Warehouse.Application.CommonDapper;
public interface IWarehousesSizeDapperRepository
{
    Task AddWarehouseSizeAsync(WarehouseSize warehouseSize);
    Task<WarehouseSize?> GetByIdAsync(Guid id);
    Task<List<WarehouseSize>> ListAsync();
    Task UpdateWarehouseSizeAsync(WarehouseSize warehouseSize);
    Task RemoveWarehouseSizeAsync(WarehouseSize warehouseSize);
}