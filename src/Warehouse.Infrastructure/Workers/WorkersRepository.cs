using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Common;
using Warehouse.Domain.Workers;
using Warehouse.Infrastructure.Common;

namespace Warehouse.Infrastructure.Products;
public class WorkersRepository : IWorkersRepository
{
    private readonly WarehouseDbContext _dbContext;

    public WorkersRepository(WarehouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddWorkerAsync(Worker worker)
    {
        await _dbContext.Workers.AddAsync(worker);
    }

    public async Task<Worker?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Workers.FindAsync(id);
    }

    public async Task<List<Worker>> ListAsync()
    {
        return await _dbContext.Workers.ToListAsync();
    }

    public Task RemoveWorkerAsync(Worker worker)
    {
        _dbContext.Remove(worker);

        return Task.CompletedTask;
    }

    public Task UpdateWorkerAsync(Worker worker)
    {
        _dbContext.Update(worker);

        return Task.CompletedTask;
    }
}
