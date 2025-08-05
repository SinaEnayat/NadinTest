using AutoMapper;
using MediatR;
using ProductManagement.Application.Common;
using ProductManagement.Application.DTOs;
using ProductManagement.Core.Entities;
using ProductManagement.Core.Interfaces;

namespace ProductManagement.Application.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, BaseResponse<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Check if product with same date already exists
            if (await _unitOfWork.Products.ExistsByProductDateAsync(request.ProductDate))
            {
                return BaseResponse<ProductDto>.Failure("A product with this date already exists.");
            }

            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                ProductDate = request.ProductDate,
                UserId = request.UserId,
                CreatedBy = request.CreatedBy
            };

            var createdProduct = await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();

            var productDto = _mapper.Map<ProductDto>(createdProduct);
            return BaseResponse<ProductDto>.Success(productDto, "Product created successfully");
        }
        catch (Exception ex)
        {
            return BaseResponse<ProductDto>.Failure($"Error creating product: {ex.Message}");
        }
    }
} 