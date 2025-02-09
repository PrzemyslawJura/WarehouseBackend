using ErrorOr;
using MediatR;
using Warehouse.Application.Common;
using Warehouse.Application.CommonDapper;
using Warehouse.Domain.WarehousesSize;

namespace Warehouse.Application.Queries.WarehousesSize.ListWarehousesSize;
public class ListWarehousesSizeQueryHandler : IRequestHandler<ListWarehousesSizeQuery, ErrorOr<List<WarehouseSize?>>>
{
    private readonly IWarehousesSizeRepository _warehousesSizeRepository;
    private readonly IWarehousesSizeDapperRepository _warehousesSizeDapperRepository;

    public ListWarehousesSizeQueryHandler(IWarehousesSizeRepository warehousesSizeRepository, IWarehousesSizeDapperRepository warehousesSizeDapperRepository)
    {
        _warehousesSizeRepository = warehousesSizeRepository;
        _warehousesSizeDapperRepository = warehousesSizeDapperRepository;
    }

    public async Task<ErrorOr<List<WarehouseSize?>>> Handle(ListWarehousesSizeQuery request, CancellationToken cancellationToken)
    {
        //var result = _warehousesSizeRepository.ListAsync();

        var result = _warehousesSizeDapperRepository.ListAsync();

        if (!result.Result.Any())
        {
            return Error.NotFound(description: "WarehouseSize not found");
        }

        return await result;
    }
}

