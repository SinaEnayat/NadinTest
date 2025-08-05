using MediatR;
using ProductManagement.Application.Common;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Queries.GetProducts;

public class GetProductsQuery : IRequest<BaseResponse<List<ProductDto>>>
{
    // This query is public - no authentication required
}