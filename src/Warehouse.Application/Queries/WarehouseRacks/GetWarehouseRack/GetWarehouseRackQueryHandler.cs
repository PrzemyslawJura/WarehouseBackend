using ErrorOr;
using MediatR;
using Warehouse.Application.Common;
using Warehouse.Application.CommonDapper;
using Warehouse.Application.Queries.WarehouseRacks.GetWarehouseRack;
using Warehouse.Domain.Warehouses;

namespace Warehouse.Application.Queries.WarehousesRacks.GetWarehouseRack;
public class GetWarehouseRackQueryHandler : IRequestHandler<GetWarehouseRackQuery, ErrorOr<WarehouseRack>>
{
    private readonly IWarehouseRacksRepository _warehousesRackRepository;
    private readonly IWarehouseRacksDapperRepository _warehouseRacksDapperRepository;

    public GetWarehouseRackQueryHandler(IWarehouseRacksRepository warehousesRackRepository, IWarehouseRacksDapperRepository warehouseRacksDapperRepository)
    {
        _warehousesRackRepository = warehousesRackRepository;
        _warehouseRacksDapperRepository = warehouseRacksDapperRepository;
    }

    public async Task<ErrorOr<WarehouseRack>> Handle(GetWarehouseRackQuery query, CancellationToken cancellationToken)
    {
        //var warehouseRack = await _warehousesRackRepository.GetByIdAsync(query.Id);

        var warehouseRack = await _warehouseRacksDapperRepository.GetByIdAsync(query.Id);

        return warehouseRack is null
            ? Error.NotFound(description: "WarehouseRack not found")
            : warehouseRack;
    }
}
