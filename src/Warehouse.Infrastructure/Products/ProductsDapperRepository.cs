using Dapper;
using Infrastructure.Persistence.Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Warehouse.Application.CommonDapper;
using Warehouse.Domain.Products;

namespace Warehouse.Infrastructure.Products;
public class ProductsDapperRepository : IProductsDapperRepository
{
    private readonly IConfiguration configuration;

    public ProductsDapperRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task AddProductAsync(Product product)
    {
        using (var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var query = "INSERT INTO Products (Id, Name, Type, Description) VALUES (@id, @name, @type, @description)";
            
            await connection.ExecuteAsync(query, new
            {
                id = product.Id.ToString(),
                name = product.Name,
                type = product.Type,
                description = product.Description
            });
        }
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        using (var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            SqlMapper.AddTypeHandler(new GuidTypeHandler());
            var query = "SELECT * FROM Products WHERE Id = @id";
            return await connection.QueryFirstOrDefaultAsync<Product>(query, new { id = id.ToString() });
        }
    }

    public async Task<List<Product>> ListAsync()
    {
        using (var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            SqlMapper.AddTypeHandler(new GuidTypeHandler());
            var query = "SELECT * FROM Products";
            var products = await connection.QueryAsync<Product>(query);
            return products.ToList();
        }
    }

    public Task RemoveProductAsync(Product product)
    {
        using (var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var query = "DELETE FROM Products WHERE Id = @id";
            connection.Execute(query, new { id = product.Id });
            return Task.CompletedTask;
        }
    }

    public Task UpdateProductAsync(Product product)
    {
        using (var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var query = "UPDATE Products SET Name = @name, Type = @type, Description = @description WHERE Id = @id";
            
            connection.ExecuteAsync(query, new
            {
                id = product.Id.ToString(),
                name = product.Name,
                type = product.Type,
                description = product.Description
            });
            return Task.CompletedTask;
        }
    }
}
