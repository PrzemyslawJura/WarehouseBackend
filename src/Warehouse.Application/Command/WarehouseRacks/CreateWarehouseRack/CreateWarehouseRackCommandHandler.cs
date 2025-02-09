using ErrorOr;
using MediatR;
using Warehouse.Application.Common;
using Warehouse.Application.CommonDapper;
using Warehouse.Domain.Warehouses;

namespace Warehouse.Application.Command.WarehouseRacks.CreateWarehouseRack;

public class CreateWarehouseRackCommandHandler : IRequestHandler<CreateWarehouseRackCommand, ErrorOr<WarehouseRack>>
{
    private readonly IWarehouseRacksRepository _warehouseRacksRepository;
    private readonly IWarehouseRacksDapperRepository _warehouseRacksDapperRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateWarehouseRackCommandHandler(
        IWarehouseRacksRepository warehouseRacksRepository,
        IWarehouseRacksDapperRepository warehouseRacksDapperRepository,
        IUnitOfWork unitOfWork)
    {
        _warehouseRacksRepository = warehouseRacksRepository;
        _warehouseRacksDapperRepository = warehouseRacksDapperRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<WarehouseRack>> Handle(CreateWarehouseRackCommand request, CancellationToken cancellationToken)
    {
        var warehouseRack = new WarehouseRack(
            sector: request.Sector,
            rack: request.Rack,
            quantity: request.Quantity,
            warehouseSizeId: request.WarehouseSizeId);

        //await _warehouseRacksRepository.AddWarehouseRackAsync(warehouseRack);
        //await _unitOfWork.CommitChangesAsync();

        await _warehouseRacksDapperRepository.AddWarehouseRackAsync(warehouseRack);

        return warehouseRack;
    }
}
