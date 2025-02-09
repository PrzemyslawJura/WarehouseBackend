using ErrorOr;
using MediatR;
using Warehouse.Application.Common;
using Warehouse.Application.CommonDapper;
using Warehouse.Domain.Workers;

namespace Warehouse.Application.Command.Workers.CreateWorker;
public class CreateWorkerCommandHandler : IRequestHandler<CreateWorkerCommand, ErrorOr<Worker>>
{
    private readonly IWorkersRepository _workersRepository;
    private readonly IWorkersDapperRepository _workersDapperRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateWorkerCommandHandler(IWorkersRepository workersRepository, IWorkersDapperRepository workersDapperRepository, IUnitOfWork unitOfWork)
    {
        _workersRepository = workersRepository;
        _workersDapperRepository = workersDapperRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Worker>> Handle(CreateWorkerCommand request, CancellationToken cancellationToken)
    {
        var worker = new Worker(
            firstName: request.FirstName,
            lastName: request.LastName,
            role: request.Role);

        //await _workersRepository.AddWorkerAsync(worker);
        //await _unitOfWork.CommitChangesAsync();

        await _workersDapperRepository.AddWorkerAsync(worker);

        return worker;
    }
}
