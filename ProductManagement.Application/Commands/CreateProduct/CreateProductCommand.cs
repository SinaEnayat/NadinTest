using MediatR;
using ProductManagement.Application.Common;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Commands.CreateProduct;

public class CreateProductCommand : IRequest<BaseResponse<ProductDto>>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime ProductDate { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
} 