using ErrorOr;
using MediatR;
using Warehouse.Application.Common;
using Warehouse.Application.CommonDapper;
using Warehouse.Domain.Workers;

namespace Warehouse.Application.Queries.Workers.GetWorker;
public class GetWorkerQueryHandler : IRequestHandler<GetWorkerQuery, ErrorOr<Worker>>
{
    private readonly IWorkersRepository _workersRepository;
    private readonly IWorkersDapperRepository _workersDapperRepository;

    public GetWorkerQueryHandler(IWorkersRepository workersRepository, IWorkersDapperRepository workersDapperRepository)
    {
        _workersRepository = workersRepository;
        _workersDapperRepository = workersDapperRepository;
    }

    public async Task<ErrorOr<Worker>> Handle(GetWorkerQuery query, CancellationToken cancellationToken)
    {
        //var worker = await _workersRepository.GetByIdAsync(query.Id);

        var worker = await _workersDapperRepository.GetByIdAsync(query.Id);

        return worker is null
            ? Error.NotFound(description: "Worker not found")
            : worker;
    }
}
