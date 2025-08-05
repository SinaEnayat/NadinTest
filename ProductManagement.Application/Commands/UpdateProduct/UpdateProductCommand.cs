using MediatR;
using ProductManagement.Application.Common;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest<BaseResponse<ProductDto>>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime ProductDate { get; set; }
    public string UserId { get; set; } = string.Empty; // For authorization check
} 