using MediatR;
using ProductManagement.Application.Common;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Queries.GetProductById;

public class GetProductByIdQuery : IRequest<BaseResponse<ProductDto>>
{
    public int Id { get; set; }
}