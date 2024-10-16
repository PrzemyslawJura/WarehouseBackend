﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Application.Command.WarehousesSize.CreateWarehouseSize;
using Warehouse.Application.Command.WarehousesSize.DeleteWarehouseSize;
using Warehouse.Application.Command.WarehousesSize.UpdateWarehouseSize;
using Warehouse.Application.Command.Workers.CreateWorker;
using Warehouse.Application.Command.Workers.DeleteWorker;
using Warehouse.Application.Command.Workers.UpdateWorker;
using Warehouse.Application.Queries.WarehousesSize.GetWarehouseSize;
using Warehouse.Application.Queries.WarehousesSize.ListWarehousesSize;
using Warehouse.Application.Queries.Workers.GetWorker;
using Warehouse.Application.Queries.Workers.ListWorkers;
using Warehouse.Contracts.WarehousesSize;
using Warehouse.Contracts.Workers;

namespace Warehouse.Api.Controllers;
[Route("[controller]")]
[ApiController]
public class AdministratorController : ApiController
{
    private readonly ISender _mediator;

    public AdministratorController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Worker")]
    public async Task<IActionResult> CreateWorker(
        CreateWorkerRequest request)
    {
        var _workerRole = ToDto(request.Role);

        var command = new CreateWorkerCommand(request.FirstName, request.LastName, _workerRole);

        var createWorkerResult = await _mediator.Send(command);

        return createWorkerResult.Match(
            worker => CreatedAtAction(
                nameof(GetWorker),
                new { WorkerId = worker.Id },
                new WorkerResponse(worker.Id, worker.FirstName, worker.LastName, worker.Role.ToString())),
            Problem);
    }

    [HttpGet("{workerId:guid}")]
    public async Task<IActionResult> GetWorker(Guid workerId)
    {
        var query = new GetWorkerQuery(workerId);

        var getWorkerResult = await _mediator.Send(query);

        return getWorkerResult.Match(
            worker => Ok(new WorkerResponse(
                worker.Id,
                worker.FirstName,
                worker.LastName,
                worker.Role.ToString())),
            Problem);
    }

    [HttpGet("Workers")]
    public async Task<IActionResult> ListWorkers()
    {
        var command = new ListWorkersQuery();

        var listWorkersResult = await _mediator.Send(command);

        return listWorkersResult.Match(
            workers => Ok(workers.ConvertAll(worker => new WorkerResponse(
                worker.Id,
                worker.FirstName,
                worker.LastName,
                worker.Role.ToString()))),
            Problem);
    }

    [HttpPut("Worker")]
    public async Task<IActionResult> UpdateWorker(UpdateWorkerRequest request)
    {
        var _workerRole = ToDto(request.Role);

        var command = new UpdateWorkerCommand(
                            request.Id,
                            request.FirstName,
                            request.LastName,
                            _workerRole);

        var updateWorkerResult = await _mediator.Send(command);

        return updateWorkerResult.Match(
                worker => Ok(new WorkerResponse(
                    worker.Id,
                    worker.FirstName,
                    worker.LastName,
                    worker.Role.ToString())),
            Problem);
    }

    [HttpDelete("{workerId:guid}")]
    public async Task<IActionResult> DeleteWorker(Guid workerId)
    {
        var command = new DeleteWorkerCommand(workerId);

        var deleteWorkerResult = await _mediator.Send(command);

        return deleteWorkerResult.Match(
            _ => NoContent(),
            Problem);
    }
    private static Domain.Workers.WorkerRole ToDto(Contracts.Workers.WorkerRole workerRole)
    {
        return workerRole switch
        {
            Contracts.Workers.WorkerRole.Admin => Domain.Workers.WorkerRole.Admin,
            Contracts.Workers.WorkerRole.Regular => Domain.Workers.WorkerRole.Regular,
            _ => throw new InvalidOperationException(),
        };
    }

    [HttpPost("WarehouseSize")]
    public async Task<IActionResult> CreateWarehouseSize(
        CreateWarehouseSizeRequest request)
    {
        var command = new CreateWarehouseSizeCommand(request.Name, request.SectorNumber, request.RackQuantity);

        var createWarehouseSizeResult = await _mediator.Send(command);

        return createWarehouseSizeResult.Match(
            warehouseSize => CreatedAtAction(
                nameof(GetWarehouseSize),
                new { WarehouseSizeId = warehouseSize.Id },
                new WarehouseSizeResponse(
                    warehouseSize.Id,
                    warehouseSize.Name,
                    warehouseSize.SectorNumber,
                    warehouseSize.RackQuantity)),
            Problem);
    }

    [HttpGet("WarehouseSize{warehouseSizeId:guid}")]
    public async Task<IActionResult> GetWarehouseSize(Guid warehouseSizeId)
    {
        var query = new GetWarehouseSizeQuery(warehouseSizeId);

        var getWarehouseSizeResult = await _mediator.Send(query);

        return getWarehouseSizeResult.Match(
            warehouseSize => Ok(new WarehouseSizeResponse(
                warehouseSize.Id,
                warehouseSize.Name,
                warehouseSize.SectorNumber,
                warehouseSize.RackQuantity)),
            Problem);
    }

    [HttpGet("WarehousesSizes")]
    public async Task<IActionResult> ListWarehouseSizes()
    {
        var command = new ListWarehousesSizeQuery();

        var listWarehouseSizesResult = await _mediator.Send(command);

        return listWarehouseSizesResult.Match(
            warehouseSizes => Ok(warehouseSizes.ConvertAll(warehouseSize => new WarehouseSizeResponse(
                warehouseSize.Id,
                warehouseSize.Name,
                warehouseSize.SectorNumber,
                warehouseSize.RackQuantity))),
            Problem);
    }

    [HttpPut("WarehouseSize")]
    public async Task<IActionResult> UpdateWarehouseSize(UpdateWarehouseSizeRequest request)
    {

        var command = new UpdateWarehouseSizeCommand(request.Id, request.Name, request.SectorNumber, request.RackQuantity);

        var updateWarehouseSizeResult = await _mediator.Send(command);

        return updateWarehouseSizeResult.Match(
                warehouseSize => Ok(new WarehouseSizeResponse(
                    warehouseSize.Id,
                    warehouseSize.Name,
                    warehouseSize.SectorNumber,
                    warehouseSize.RackQuantity)),
            Problem);
    }

    [HttpDelete("WarehouseSize{warehouseSizeId:guid}")]
    public async Task<IActionResult> DeleteWarehouseSize(Guid warehouseSizeId)
    {
        var command = new DeleteWarehouseSizeCommand(warehouseSizeId);

        var deleteWarehouseSizeResult = await _mediator.Send(command);

        return deleteWarehouseSizeResult.Match(
            _ => NoContent(),
            Problem);
    }
}
