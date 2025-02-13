using ErrorOr;
using MediatR;
using Warehouse.Application.Common;
using Warehouse.Application.CommonDapper;
using Warehouse.Domain.Transactions;
using Warehouse.Domain.Warehouses;

namespace Warehouse.Application.Command.Transactions.CreateTransactionCommand;
public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, ErrorOr<Transaction>>
{
    private readonly ITransactionsRepository _transactionsRepository;
    private readonly IWarehouseRacksRepository _warehouseRacksRepository;
    private readonly ITransactionsDapperRepository _transactionsDapperRepository;
    private readonly IWarehouseRacksDapperRepository _warehouseRacksDapperRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTransactionCommandHandler(
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

    public async Task<ErrorOr<Transaction>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var warehouseRack = new WarehouseRack(
            sector: request.Sector,
            rack: request.Rack,
            quantity: request.TransactionQuantity,
            warehouseSizeId: request.WarehouseSizeId);

        var transaction = new Transaction(
            quantity: request.TransactionQuantity,
            type: request.TransactionType,
            date: request.Date,
            productId: request.ProductId,
            warehouseRackId: warehouseRack.Id,
            workerId: request.WorkerId);

        //await _warehouseRacksRepository.AddWarehouseRackAsync(warehouseRack);
        //await _transactionsRepository.AddTransactionAsync(transaction);
        //await _unitOfWork.CommitChangesAsync();
        
        await _warehouseRacksDapperRepository.AddWarehouseRackAsync(warehouseRack);
        await _transactionsDapperRepository.AddTransactionAsync(transaction);
        

        return transaction;
    }
}
