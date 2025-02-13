using ErrorOr;
using MediatR;
using Warehouse.Application.Common;
using Warehouse.Application.CommonDapper;
using Warehouse.Domain.Transactions;
using Warehouse.Domain.Warehouses;

namespace Warehouse.Application.Command.Transactions.UpdateTransactionCommand;
public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, ErrorOr<Transaction>>
{
    private readonly ITransactionsRepository _transactionsRepository;
    private readonly IWarehouseRacksRepository _warehouseRacksRepository;
    private readonly ITransactionsDapperRepository _transactionsDapperRepository;
    private readonly IWarehouseRacksDapperRepository _warehouseRacksDapperRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTransactionCommandHandler(
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

    public async Task<ErrorOr<Transaction>> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        var warehouseRack = new WarehouseRack(
            id: request.WarehouseRackId,
            sector: request.Sector,
            rack: request.Rack,
            quantity: request.TransactionQuantity,
            warehouseSizeId: request.WarehouseSizeId);

        var transaction = new Transaction(
            id: request.TransactionId,
            quantity: request.TransactionQuantity,
            type: request.TransactionType,
            date: request.Date,
            productId: request.ProductId,
            warehouseRackId: warehouseRack.Id,
            workerId: request.WorkerId);
        
        //await _transactionsRepository.UpdateTransactionAsync(transaction);
        //await _warehouseRacksRepository.UpdateWarehouseRackAsync(warehouseRack);
        //await _unitOfWork.CommitChangesAsync();
        
        await _transactionsDapperRepository.UpdateTransactionAsync(transaction);
        await _warehouseRacksDapperRepository.UpdateWarehouseRackAsync(warehouseRack);
        
        
        return transaction;
    }
}

