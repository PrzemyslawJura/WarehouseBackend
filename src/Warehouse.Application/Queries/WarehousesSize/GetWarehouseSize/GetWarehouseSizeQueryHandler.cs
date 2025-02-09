using ErrorOr;
using MediatR;
using Warehouse.Application.Common;
using Warehouse.Application.CommonDapper;
using Warehouse.Domain.WarehousesSize;

namespace Warehouse.Application.Queries.WarehousesSize.GetWarehouseSize;
public class GetWarehouseSizeQueryHandler : IRequestHandler<GetWarehouseSizeQuery, ErrorOr<WarehouseSize>>
{
    private readonly IWarehousesSizeRepository _warehousesSizeRepository;
    private readonly IWarehousesSizeDapperRepository _warehousesSizeDapperRepository;

    public GetWarehouseSizeQueryHandler(IWarehousesSizeRepository warehousesSizeRepository, IWarehousesSizeDapperRepository warehousesSizeDapperRepository)
    {
        _warehousesSizeRepository = warehousesSizeRepository;
        _warehousesSizeDapperRepository = warehousesSizeDapperRepository;
    }

    public async Task<ErrorOr<WarehouseSize>> Handle(GetWarehouseSizeQuery query, CancellationToken cancellationToken)
    {
        //var warehouseSize = await _warehousesSizeRepository.GetByIdAsync(query.Id);

        var warehouseSize = await _warehousesSizeDapperRepository.GetByIdAsync(query.Id);

        return warehouseSize is null
            ? Error.NotFound(description: "WarehouseSize not found")
            : warehouseSize;
    }
}
