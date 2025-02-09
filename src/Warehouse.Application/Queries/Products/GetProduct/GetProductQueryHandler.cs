using ErrorOr;
using MediatR;
using Warehouse.Application.Common;
using Warehouse.Application.CommonDapper;
using Warehouse.Domain.Products;

namespace Warehouse.Application.Queries.Products.GetProduct;
public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ErrorOr<Product>>
{
    private readonly IProductsRepository _productsRepository;
    private readonly IProductsDapperRepository _productsDapperRepository;

    public GetProductQueryHandler(IProductsRepository productsRepository, IProductsDapperRepository productsDapperRepository)
    {
        _productsRepository = productsRepository;
        _productsDapperRepository = productsDapperRepository;
    }

    public async Task<ErrorOr<Product>> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        //var product = await _productsRepository.GetByIdAsync(query.Id);

        var product = await _productsDapperRepository.GetByIdAsync(query.Id);

        return product is null
            ? Error.NotFound(description: "Product not found")
            : product;
    }
}
