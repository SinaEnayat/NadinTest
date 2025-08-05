using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.Commands.CreateProduct;
using ProductManagement.Application.Commands.DeleteProduct;
using ProductManagement.Application.Commands.UpdateProduct;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Queries.GetProductById;
using ProductManagement.Application.Queries.GetProducts;
using ProductManagement.Application.Queries.GetProductsByUser;

namespace ProductManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/products
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var query = new GetProductsQuery();
        var result = await _mediator.Send(query);
        
        if (result.IsSuccess)
            return Ok(result);
        
        return BadRequest(result);
    }

    // GET: api/products/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var query = new GetProductByIdQuery { Id = id };
        var result = await _mediator.Send(query);
        
        if (result.IsSuccess)
            return Ok(result);
        
        return NotFound(result);
    }

    // GET: api/products/user/{userId}
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetProductsByUser(string userId)
    {
        var query = new GetProductsByUserQuery { UserId = userId };
        var result = await _mediator.Send(query);
        
        if (result.IsSuccess)
            return Ok(result);
        
        return BadRequest(result);
    }

    // POST: api/products
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
    {
        // For now, we'll use hardcoded values for UserId and CreatedBy
        // In a real application, these would come from JWT token
        var command = new CreateProductCommand
        {
            Name = createProductDto.Name,
            Description = createProductDto.Description,
            Price = createProductDto.Price,
            ProductDate = createProductDto.ProductDate,
            UserId = "user123", // This would come from JWT token
            CreatedBy = "John Doe" // This would come from JWT token
        };

        var result = await _mediator.Send(command);
        
        if (result.IsSuccess)
            return CreatedAtAction(nameof(GetProduct), new { id = result.Data!.Id }, result);
        
        return BadRequest(result);
    }

    // PUT: api/products/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto updateProductDto)
    {
        var command = new UpdateProductCommand
        {
            Id = id,
            Name = updateProductDto.Name,
            Description = updateProductDto.Description,
            Price = updateProductDto.Price,
            ProductDate = updateProductDto.ProductDate,
            UserId = "user123" // This would come from JWT token
        };

        var result = await _mediator.Send(command);
        
        if (result.IsSuccess)
            return Ok(result);
        
        return BadRequest(result);
    }

    // DELETE: api/products/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var command = new DeleteProductCommand
        {
            Id = id,
            UserId = "user123" // This would come from JWT token
        };

        var result = await _mediator.Send(command);
        
        if (result.IsSuccess)
            return Ok(result);
        
        return BadRequest(result);
    }
} 