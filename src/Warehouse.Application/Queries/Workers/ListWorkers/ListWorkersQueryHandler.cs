using ErrorOr;
using MediatR;
using Warehouse.Application.Common;
using Warehouse.Application.CommonDapper;
using Warehouse.Domain.Workers;

namespace Warehouse.Application.Queries.Workers.ListWorkers;
public class ListWorkersQueryHandler : IRequestHandler<ListWorkersQuery, ErrorOr<List<Worker?>>>
{
    private readonly IWorkersRepository _workersRepository;
    private readonly IWorkersDapperRepository _workersDapperRepository;

    public ListWorkersQueryHandler(IWorkersRepository workersRepository, IWorkersDapperRepository workersDapperRepository)
    {
        _workersRepository = workersRepository;
        _workersDapperRepository = workersDapperRepository;
    }

    public async Task<ErrorOr<List<Worker?>>> Handle(ListWorkersQuery request, CancellationToken cancellationToken)
    {
        //var result = _workersRepository.ListAsync();

        var result = _workersDapperRepository.ListAsync();

        if (!result.Result.Any())
        {
            return Error.NotFound(description: "Workers not found");
        }

        return await result;
    }
}
