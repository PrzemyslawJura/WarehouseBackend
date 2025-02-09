using ErrorOr;
using MediatR;
using Warehouse.Application.Common;
using Warehouse.Application.CommonDapper;

namespace Warehouse.Application.Command.WarehousesSize.DeleteWarehouseSize;
public class DeleteWarehouseSizeCommandHandler : IRequestHandler<DeleteWarehouseSizeCommand, ErrorOr<Deleted>>
{
    private readonly IWarehousesSizeRepository _warehousesSizeRepository;
    private readonly IWarehousesSizeDapperRepository _warehousesSizeDapperRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteWarehouseSizeCommandHandler(
        IWarehousesSizeRepository warehousesSizeRepository,
        IWarehousesSizeDapperRepository warehousesSizeDapperRepository,
        IUnitOfWork unitOfWork)
    {
        _warehousesSizeRepository = warehousesSizeRepository;
        _warehousesSizeDapperRepository = warehousesSizeDapperRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteWarehouseSizeCommand command, CancellationToken cancellationToken)
    {
        //var warehouseSize = await _warehousesSizeRepository.GetByIdAsync(command.Id);

        var warehouseSize = await _warehousesSizeDapperRepository.GetByIdAsync(command.Id);

        if (warehouseSize is null)
        {
            return Error.NotFound(description: "WarehouseSize not found");
        }

        //await _warehousesSizeRepository.RemoveWarehouseSizeAsync(warehouseSize);
        //await _unitOfWork.CommitChangesAsync();

        await _warehousesSizeDapperRepository.RemoveWarehouseSizeAsync(warehouseSize);

        return Result.Deleted;
    }
}

