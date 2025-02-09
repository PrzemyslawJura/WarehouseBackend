using ErrorOr;
using MediatR;
using Warehouse.Application.Common;
using Warehouse.Application.CommonDapper;
using Warehouse.Domain.Products;

namespace Warehouse.Application.Queries.Products.ListProducts;
public class ListProductsQueryHandler : IRequestHandler<ListProductsQuery, ErrorOr<List<Product?>>>
{
    private readonly IProductsRepository _productsRepository;
    private readonly IProductsDapperRepository _productsDapperRepository;

    public ListProductsQueryHandler(IProductsRepository productsRepository, IProductsDapperRepository productsDapperRepository)
    {
        _productsRepository = productsRepository;
        _productsDapperRepository = productsDapperRepository;
    }

    public async Task<ErrorOr<List<Product?>>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
    {
        //var result = _productsRepository.ListAsync();

        var result = _productsDapperRepository.ListAsync();

        if (!result.Result.Any())
        {
            return Error.NotFound(description: "Products not found");
        }

        return await result;
    }
}