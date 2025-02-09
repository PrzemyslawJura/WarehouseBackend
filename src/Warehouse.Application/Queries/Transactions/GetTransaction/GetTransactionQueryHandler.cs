using ErrorOr;
using MediatR;
using Warehouse.Application.Common;
using Warehouse.Application.CommonDapper;
using Warehouse.Domain.Transactions;

namespace Warehouse.Application.Queries.Transactions.GetTransaction;
public class GetTransactionQueryHandler : IRequestHandler<GetTransactionQuery, ErrorOr<Transaction>>
{
    private readonly ITransactionsRepository _transactionsRepository;
    private readonly ITransactionsDapperRepository _transactionsDapperRepository;

    public GetTransactionQueryHandler(ITransactionsRepository transactionsRepository, ITransactionsDapperRepository transactionsDapperRepository)
    {
        _transactionsRepository = transactionsRepository;
        _transactionsDapperRepository = transactionsDapperRepository;
    }

    public async Task<ErrorOr<Transaction>> Handle(GetTransactionQuery query, CancellationToken cancellationToken)
    {
        //var transaction = await _transactionsRepository.GetByIdAsync(query.Id);

        var transaction = await _transactionsDapperRepository.GetByIdAsync(query.Id);

        return transaction is null
            ? Error.NotFound(description: "Worker not found")
            : transaction;
    }
}
