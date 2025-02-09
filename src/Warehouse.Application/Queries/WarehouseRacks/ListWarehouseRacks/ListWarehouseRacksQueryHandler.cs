using ErrorOr;
using MediatR;
using Warehouse.Application.Common;
using Warehouse.Application.CommonDapper;
using Warehouse.Domain.Warehouses;

namespace Warehouse.Application.Queries.WarehouseRacks.ListWarehouseRacks;
public class ListWarehouseRacksQueryHandler : IRequestHandler<ListWarehouseRacksQuery, ErrorOr<List<WarehouseRack?>>>
{
    private readonly IWarehouseRacksRepository _warehouseRacksRepository;
    private readonly IWarehouseRacksDapperRepository _warehouseRacksDapperRepository;

    public ListWarehouseRacksQueryHandler(IWarehouseRacksRepository warehousesRackRepository, IWarehouseRacksDapperRepository warehouseRacksDapperRepository)
    {
        _warehouseRacksRepository = warehousesRackRepository;
        _warehouseRacksDapperRepository = warehouseRacksDapperRepository;
    }

    public async Task<ErrorOr<List<WarehouseRack?>>> Handle(ListWarehouseRacksQuery request, CancellationToken cancellationToken)
    {
        //var result = _warehouseRacksRepository.ListAsync();

        var result = _warehouseRacksDapperRepository.ListAsync();

        if (!result.Result.Any())
        {
            return Error.NotFound(description: "Warehouse Racks not found");
        }

        return await result;
    }
}

