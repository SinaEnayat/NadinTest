namespace ProductManagement.Core.Exceptions;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException(int productId) 
        : base($"Product with ID {productId} was not found.")
    {
        ProductId = productId;
    }
    
    public int ProductId { get; }
} 