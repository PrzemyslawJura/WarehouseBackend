using ErrorOr;
using MediatR;
using Warehouse.Application.Common;
using Warehouse.Application.CommonDapper;

namespace Warehouse.Application.Command.Products.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ErrorOr<Deleted>>
{
    private readonly IProductsRepository _productsRepository;
    private readonly IProductsDapperRepository _productsDapperRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IProductsRepository productsRepository, IProductsDapperRepository productsDapperRepository, IUnitOfWork unitOfWork)
    {
        _productsRepository = productsRepository;
        _productsDapperRepository = productsDapperRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        //var product = await _productsRepository.GetByIdAsync(command.Id);

        var product = await _productsDapperRepository.GetByIdAsync(command.Id);

        if (product is null)
        {
            return Error.NotFound(description: "Product not found");
        }

        //await _productsRepository.RemoveProductAsync(product);
        //await _unitOfWork.CommitChangesAsync();

        await _productsDapperRepository.RemoveProductAsync(product);

        return Result.Deleted;
    }
}
