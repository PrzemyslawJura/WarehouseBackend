using ErrorOr;
using MediatR;
using Warehouse.Application.Common;
using Warehouse.Application.CommonDapper;

namespace Warehouse.Application.Command.Transactions.DeleteTransactionCommand;
public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, ErrorOr<Deleted>>
{
    private readonly ITransactionsRepository _transactionsRepository;
    private readonly IWarehouseRacksRepository _warehouseRacksRepository;
    private readonly ITransactionsDapperRepository _transactionsDapperRepository;
    private readonly IWarehouseRacksDapperRepository _warehouseRacksDapperRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTransactionCommandHandler(
        ITransactionsRepository transactionsRepository,
        IWarehouseRacksDapperRepository warehouseRacksDapperRepository,
        ITransactionsDapperRepository transactionsDapperRepository,
        IWarehouseRacksRepository warehouseRacksRepository,
        IUnitOfWork unitOfWork)
    {
        _transactionsRepository = transactionsRepository;
        _warehouseRacksRepository = warehouseRacksRepository;
        _transactionsDapperRepository = transactionsDapperRepository;
        _warehouseRacksDapperRepository = warehouseRacksDapperRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteTransactionCommand command, CancellationToken cancellationToken)
    {
        //var transaction = await _transactionsRepository.GetByIdAsync(command.Id);
        
        var transaction = await _transactionsDapperRepository.GetByIdAsync(command.Id);

        if (transaction is null)
        {
            return Error.NotFound(description: "Transaction not found");
        }

        //var warehouseRack = await _warehouseRacksRepository.GetByIdAsync(transaction.WarehouseRackId);
        
        var warehouseRack = await _warehouseRacksDapperRepository.GetByIdAsync(transaction.WarehouseRackId);

        if (warehouseRack is null)
        {
            return Error.NotFound(description: "WarehouseRack not found");
        }

        //await _transactionsRepository.RemoveTransactionAsync(transaction);
        //await _warehouseRacksRepository.RemoveWarehouseRackAsync(warehouseRack);
        //await _unitOfWork.CommitChangesAsync();
        
        await _transactionsDapperRepository.RemoveTransactionAsync(transaction);
        await _warehouseRacksDapperRepository.RemoveWarehouseRackAsync(warehouseRack);

        return Result.Deleted;
    }
}