using ErrorOr;
using MediatR;
using Warehouse.Application.Common;
using Warehouse.Application.CommonDapper;
using Warehouse.Domain.Workers;

namespace Warehouse.Application.Command.Workers.UpdateWorker;
public class UpdateWorkerCommandHandler : IRequestHandler<UpdateWorkerCommand, ErrorOr<Worker>>
{
    private readonly IWorkersRepository _workersRepository;
    private readonly IWorkersDapperRepository _workersDapperRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateWorkerCommandHandler(IWorkersRepository workersRepository, IWorkersDapperRepository workersDapperRepository, IUnitOfWork unitOfWork)
    {
        _workersRepository = workersRepository;
        _workersDapperRepository = workersDapperRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Worker>> Handle(UpdateWorkerCommand request, CancellationToken cancellationToken)
    {
        var worker = new Worker(
            id: request.Id,
            firstName: request.FirstName,
            lastName: request.LastName,
            role: request.Role);

        //await _workersRepository.UpdateWorkerAsync(worker);
        //await _unitOfWork.CommitChangesAsync();

        await _workersDapperRepository.UpdateWorkerAsync(worker);

        return worker;
    }
}
