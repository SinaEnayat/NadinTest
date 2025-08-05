using MediatR;
using ProductManagement.Application.Common;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Queries.GetProductsByUser;

public class GetProductsByUserQuery : IRequest<BaseResponse<List<ProductDto>>>
{
    public string UserId { get; set; } = string.Empty;
}
