using Dapper;
using Infrastructure.Persistence.Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Warehouse.Application.CommonDapper;
using Warehouse.Domain.Products;
using Warehouse.Domain.Transactions;
using Warehouse.Domain.Warehouses;
using Warehouse.Domain.WarehousesSize;
using Warehouse.Domain.Workers;

namespace Warehouse.Infrastructure.Transactions;
public class TransactionsDapperRepository : ITransactionsDapperRepository
{
    private readonly IConfiguration configuration;

    public TransactionsDapperRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    
    public async Task AddTransactionAsync(Transaction transaction)
    {
        using (var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var query = "INSERT INTO Transactions (Id, Quantity, Type, Date, ProductId, WarehouseRackId, WorkerId)"
                +"VALUES (@id, @quantity, @type, @date, @productId, @warehouseRackId, @workerId)";
            
            await connection.ExecuteAsync(query, new
            {
                id = transaction.Id.ToString(),
                quantity = transaction.Quantity,
                type = transaction.Type,
                date = transaction.Date,
                productId = transaction.ProductId.ToString(),
                warehouseRackId = transaction.WarehouseRackId.ToString(),
                workerId = transaction.WorkerId.ToString()
            });
        }
    }

    public async Task<Transaction?> GetByIdAsync(Guid id)
    {
        using (var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            /*connection.Open();
            SqlMapper.AddTypeHandler(new GuidTypeHandler());
            var query =
                "SELECT * "
                + "FROM Transactions t "
                + "INNER JOIN Products p ON t.ProductId = p.Id "
                + "INNER JOIN WarehouseRacks wr ON t.WarehouseRackId = wr.Id "
                + "INNER JOIN WarehousesSize ws ON wr.WarehouseSizeId = ws.Id "
                + "INNER JOIN Workers w ON t.WorkerId = w.Id "
                + "WHERE t.Id = @id;";
            
            Transaction transactionResult = null;
            
            var result = await connection.QueryAsync<Transaction, Product, WarehouseRack, WarehouseSize, Worker, Transaction>(
                query,
                (transaction, product, warehouseRack, warehouseSize, worker) =>
                {
                    transactionResult.Id = transaction.Id;
                    transactionResult.Quantity = transaction.Quantity;
                    transactionResult.Type = transaction.Type;
                    transactionResult.Date = transaction.Date;
                    transactionResult.ProductId = transaction.ProductId;
                    transactionResult.WarehouseRackId = transaction.WarehouseRackId;
                    transactionResult.WorkerId = transaction.WorkerId;
                    transactionResult.Products = product;
                    transactionResult.WarehouseRacks = warehouseRack;
                    transactionResult.WarehouseRacks.WarehousesSize = warehouseSize;
                    transactionResult.Workers = worker;

                    return transactionResult;
                },
                new { id },
                splitOn: "Id,Id,Id,Id"
            );
            
            return result.FirstOrDefault();
            
            //return await connection.QueryFirstOrDefaultAsync<Transaction>(query, new { id = id });*/
            
            connection.Open();
            SqlMapper.AddTypeHandler(new GuidTypeHandler());
            var query = @"SELECT * FROM Transactions;"
                        + "SELECT * FROM Products;"
                        + "SELECT * FROM WarehouseRacks;"
                        + "SELECT * FROM WarehousesSize;"
                        + "SELECT * FROM Workers;";
            
            using var multi = await connection.QueryMultipleAsync(query);
            
            var transactions = (await multi.ReadAsync<Transaction>()).ToList();
            var products = (await multi.ReadAsync<Product>()).ToList();
            var warehouseRacks = (await multi.ReadAsync<WarehouseRack>()).ToList();
            var warehousesSizes = (await multi.ReadAsync<WarehouseSize>()).ToList();
            var workers = (await multi.ReadAsync<Worker>()).ToList();
            
            foreach (var transaction in transactions)
            {
                transaction.Products = products.FirstOrDefault(p => p.Id == transaction.ProductId);
                transaction.WarehouseRacks = warehouseRacks.FirstOrDefault(wr => wr.Id == transaction.WarehouseRackId);
                transaction.WarehouseRacks.WarehousesSize = warehousesSizes.FirstOrDefault(ws => ws.Id == transaction.WarehouseRacks.WarehouseSizeId);
                transaction.Workers = workers.FirstOrDefault(w => w.Id == transaction.WorkerId);
            }

            return transactions.Find(x => x.Id == id);
        }
    }

    public async Task<List<Transaction>> ListAsync()
    {
        using (var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            SqlMapper.AddTypeHandler(new GuidTypeHandler());
            var query = @"SELECT * FROM Transactions;"
                        + "SELECT * FROM Products;"
                        + "SELECT * FROM WarehouseRacks;"
                        + "SELECT * FROM WarehousesSize;"
                        + "SELECT * FROM Workers;";
            
            using var multi = await connection.QueryMultipleAsync(query);
            
            var transactions = (await multi.ReadAsync<Transaction>()).ToList();
            var products = (await multi.ReadAsync<Product>()).ToList();
            var warehouseRacks = (await multi.ReadAsync<WarehouseRack>()).ToList();
            var warehousesSizes = (await multi.ReadAsync<WarehouseSize>()).ToList();
            var workers = (await multi.ReadAsync<Worker>()).ToList();
            
            foreach (var transaction in transactions)
            {
                transaction.Products = products.FirstOrDefault(p => p.Id == transaction.ProductId);
                transaction.WarehouseRacks = warehouseRacks.FirstOrDefault(wr => wr.Id == transaction.WarehouseRackId);
                transaction.WarehouseRacks.WarehousesSize = warehousesSizes.FirstOrDefault(ws => ws.Id == transaction.WarehouseRacks.WarehouseSizeId);
                transaction.Workers = workers.FirstOrDefault(w => w.Id == transaction.WorkerId);
            }
            
            return transactions.ToList();
        }
    }

    public Task RemoveTransactionAsync(Transaction transaction)
    {
        using (var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var query = "DELETE FROM Transactions WHERE Id = @id";
            connection.Execute(query, new { id = transaction.Id.ToString() });
            return Task.CompletedTask;
        }
    }

    public Task UpdateTransactionAsync(Transaction transaction)
    {
        using (var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var query = "UPDATE Transactions "
                +"SET Quantity = @quantity, Type = @type, Date = @date, ProductId = @productId, WarehouseRackId = @warehouseRackId, WorkerId = @workerId "
                +"WHERE Id = @id";
            
            connection.ExecuteAsync(query, new
            {
                id = transaction.Id.ToString(),
                quantity = transaction.Quantity,
                type = transaction.Type,
                date = transaction.Date,
                productId = transaction.ProductId.ToString(),
                warehouseRackId = transaction.WarehouseRackId.ToString(),
                workerId = transaction.WorkerId.ToString()
            });
            return Task.CompletedTask;
        }
    }
}
