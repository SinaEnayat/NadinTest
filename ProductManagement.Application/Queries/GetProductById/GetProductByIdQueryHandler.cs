using AutoMapper;
using MediatR;
using ProductManagement.Application.Common;
using ProductManagement.Application.DTOs;
using ProductManagement.Core.Interfaces;

namespace ProductManagement.Application.Queries.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, BaseResponse<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _unitOfWork.Products.GetByIdAsync(request.Id);
            
            if (product == null)
            {
                return BaseResponse<ProductDto>.Failure($"Product with ID {request.Id} not found.");
            }

            var productDto = _mapper.Map<ProductDto>(product);
            return BaseResponse<ProductDto>.Success(productDto, "Product retrieved successfully");
        }
        catch (Exception ex)
        {
            return BaseResponse<ProductDto>.Failure($"Error retrieving product: {ex.Message}");
        }
    }
} 