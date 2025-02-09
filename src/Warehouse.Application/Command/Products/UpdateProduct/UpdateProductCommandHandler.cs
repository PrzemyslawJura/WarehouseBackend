using ErrorOr;
using MediatR;
using Warehouse.Application.Common;
using Warehouse.Application.CommonDapper;
using Warehouse.Domain.Products;

namespace Warehouse.Application.Command.Products.UpdateProduct;
public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ErrorOr<Product>>
{
    private readonly IProductsRepository _productsRepository;
    private readonly IProductsDapperRepository _productsDapperRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IProductsRepository productsRepository, IProductsDapperRepository productsDapperRepository, IUnitOfWork unitOfWork)
    {
        _productsRepository = productsRepository;
        _productsDapperRepository = productsDapperRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Product>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(
            id: request.Id,
            name: request.Name,
            type: request.Type,
            description: request.Description);

        //await _productsRepository.UpdateProductAsync(product);
        //await _unitOfWork.CommitChangesAsync();

        await _productsDapperRepository.UpdateProductAsync(product);

        return product;
    }
}