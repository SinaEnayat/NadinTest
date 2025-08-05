using MediatR;
using ProductManagement.Application.Common;

namespace ProductManagement.Application.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest<BaseResponse<bool>>
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty; // For authorization check
}