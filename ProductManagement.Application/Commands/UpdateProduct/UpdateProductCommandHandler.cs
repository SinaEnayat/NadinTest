using AutoMapper;
using MediatR;
using ProductManagement.Application.Common;
using ProductManagement.Application.DTOs;
using ProductManagement.Core.Entities;
using ProductManagement.Core.Interfaces;

namespace ProductManagement.Application.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, BaseResponse<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Get existing product
            var existingProduct = await _unitOfWork.Products.GetByIdAsync(request.Id);
            if (existingProduct == null)
            {
                return BaseResponse<ProductDto>.Failure($"Product with ID {request.Id} not found.");
            }

            // Check if user can modify this product (only creator can modify)
            if (!existingProduct.CanBeModifiedBy(request.UserId))
            {
                return BaseResponse<ProductDto>.Failure("You are not authorized to modify this product.");
            }

            // Check if new ProductDate conflicts with existing products (excluding current product)
            var existingProductWithSameDate = await _unitOfWork.Products.GetByIdAsync(request.Id);
            if (existingProductWithSameDate != null && 
                existingProductWithSameDate.Id != request.Id && 
                existingProductWithSameDate.ProductDate.Date == request.ProductDate.Date)
            {
                return BaseResponse<ProductDto>.Failure("A product with this date already exists.");
            }

            // Update the product
            existingProduct.UpdateProduct(
                request.Name,
                request.Description,
                request.Price,
                request.ProductDate
            );

            var updatedProduct = await _unitOfWork.Products.UpdateAsync(existingProduct);
            await _unitOfWork.SaveChangesAsync();

            var productDto = _mapper.Map<ProductDto>(updatedProduct);
            return BaseResponse<ProductDto>.Success(productDto, "Product updated successfully");
        }
        catch (Exception ex)
        {
            return BaseResponse<ProductDto>.Failure($"Error updating product: {ex.Message}");
        }
    }
} 