using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.Application.Common;
using Warehouse.Application.CommonDapper;
using Warehouse.Infrastructure.Common;
using Warehouse.Infrastructure.Products;
using Warehouse.Infrastructure.Transactions;
using Warehouse.Infrastructure.WarehouseRacks;
using Warehouse.Infrastructure.WarehousesSize;
using Warehouse.Infrastructure.Workers;

namespace Warehouse.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddPersistence(configuration);
    }
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WarehouseDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        //options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IProductsRepository, ProductsRepository>();
        services.AddScoped<ITransactionsRepository, TransactionsRepository>();
        services.AddScoped<IWarehouseRacksRepository, WarehouseRacksRepository>();
        services.AddScoped<IWarehousesSizeRepository, WarehousesSizeRepository>();
        services.AddScoped<IWorkersRepository, WorkersRepository>();

        services.AddScoped<IProductsDapperRepository, ProductsDapperRepository>();
        services.AddScoped<ITransactionsDapperRepository, TransactionsDapperRepository>();
        services.AddScoped<IWarehouseRacksDapperRepository, WarehouseRacksDapperRepository>();
        services.AddScoped<IWarehousesSizeDapperRepository, WarehousesSizeDapperRepository>();
        services.AddScoped<IWorkersDapperRepository, WorkersDapperRepository>();


        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<WarehouseDbContext>());
        return services;
    }
}