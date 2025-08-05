using AutoMapper;
using MediatR;
using ProductManagement.Application.Common;
using ProductManagement.Application.DTOs;
using ProductManagement.Core.Interfaces;

namespace ProductManagement.Application.Queries.GetProducts;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, BaseResponse<List<ProductDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<List<ProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            var productDtos = _mapper.Map<List<ProductDto>>(products);
            
            return BaseResponse<List<ProductDto>>.Success(productDtos, "Products retrieved successfully");
        }
        catch (Exception ex)
        {
            return BaseResponse<List<ProductDto>>.Failure($"Error retrieving products: {ex.Message}");
        }
    }
} 