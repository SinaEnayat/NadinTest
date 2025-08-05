using AutoMapper;
using MediatR;
using ProductManagement.Application.Common;
using ProductManagement.Application.DTOs;
using ProductManagement.Core.Interfaces;

namespace ProductManagement.Application.Queries.GetProductsByUser;

public class GetProductsByUserQueryHandler : IRequestHandler<GetProductsByUserQuery, BaseResponse<List<ProductDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductsByUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<List<ProductDto>>> Handle(GetProductsByUserQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var products = await _unitOfWork.Products.GetByUserIdAsync(request.UserId);
            var productDtos = _mapper.Map<List<ProductDto>>(products);
            
            return BaseResponse<List<ProductDto>>.Success(productDtos, "Products retrieved successfully");
        }
        catch (Exception ex)
        {
            return BaseResponse<List<ProductDto>>.Failure($"Error retrieving products: {ex.Message}");
        }
    }
} 