using ErrorOr;
using MediatR;
using Warehouse.Application.Common;
using Warehouse.Application.CommonDapper;
using Warehouse.Domain.WarehousesSize;

namespace Warehouse.Application.Command.WarehousesSize.UpdateWarehouseSize;
public class UpdateWarehouseSizeCommandHandler : IRequestHandler<UpdateWarehouseSizeCommand, ErrorOr<WarehouseSize>>
{
    private readonly IWarehousesSizeRepository _warehousesSizeRepository;
    private readonly IWarehousesSizeDapperRepository _warehousesSizeDapperRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateWarehouseSizeCommandHandler(
        IWarehousesSizeRepository warehousesSizeRepository,
        IWarehousesSizeDapperRepository warehousesSizeDapperRepository,
        IUnitOfWork unitOfWork)
    {
        _warehousesSizeRepository = warehousesSizeRepository;
        _warehousesSizeDapperRepository = warehousesSizeDapperRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<WarehouseSize>> Handle(UpdateWarehouseSizeCommand request, CancellationToken cancellationToken)
    {
        var warehouseSize = new WarehouseSize(
            id: request.Id,
            name: request.Name,
            sectorNumber: request.SectorNumber,
            rackQuantity: request.RackQuantity);

        //await _warehousesSizeRepository.UpdateWarehouseSizeAsync(warehouseSize);
        //await _unitOfWork.CommitChangesAsync();

        await _warehousesSizeDapperRepository.UpdateWarehouseSizeAsync(warehouseSize);

        return warehouseSize;
    }
}
