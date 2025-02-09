using ErrorOr;
using MediatR;
using Warehouse.Application.Common;
using Warehouse.Application.CommonDapper;
using Warehouse.Domain.Transactions;

namespace Warehouse.Application.Queries.Transactions.ListTransactions;


public class ListTransactionsQueryHandler : IRequestHandler<ListTransactionsQuery, ErrorOr<List<Transaction?>>>
{
    private readonly ITransactionsRepository _transactionsRepository;
    private readonly ITransactionsDapperRepository _transactionsDapperRepository;

    public ListTransactionsQueryHandler(ITransactionsRepository transactionsRepository, ITransactionsDapperRepository transactionsDapperRepository)
    {
        _transactionsRepository = transactionsRepository;
        _transactionsDapperRepository = transactionsDapperRepository;
    }

    public async Task<ErrorOr<List<Transaction?>>> Handle(ListTransactionsQuery request, CancellationToken cancellationToken)
    {
        //var result = _transactionsRepository.ListAsync();

        var result = _transactionsDapperRepository.ListAsync();

        //if (!result.Result.Any())
        //{
        //   return Error.NotFound(description: "Transactions not found");
        //}
        
        return await result;
    }
}
