using MediatR;
using ProductManagement.Application.Common;
using ProductManagement.Core.Entities;
using ProductManagement.Core.Interfaces;

namespace ProductManagement.Application.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Get existing product
            var existingProduct = await _unitOfWork.Products.GetByIdAsync(request.Id);
            if (existingProduct == null)
            {
                return BaseResponse<bool>.Failure($"Product with ID {request.Id} not found.");
            }

            // Check if user can delete this product (only creator can delete)
            if (!existingProduct.CanBeModifiedBy(request.UserId))
            {
                return BaseResponse<bool>.Failure("You are not authorized to delete this product.");
            }

            // Delete the product
            await _unitOfWork.Products.DeleteAsync(request.Id);
            await _unitOfWork.SaveChangesAsync();

            return BaseResponse<bool>.Success(true, "Product deleted successfully");
        }
        catch (Exception ex)
        {
            return BaseResponse<bool>.Failure($"Error deleting product: {ex.Message}");
        }
    }
} 