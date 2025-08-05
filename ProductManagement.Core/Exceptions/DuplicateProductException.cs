namespace ProductManagement.Core.Exceptions;

public class DuplicateProductException : Exception
{
    public DuplicateProductException(DateTime productDate) 
        : base($"A product with the date {productDate:yyyy-MM-dd} already exists.")
    {
        ProductDate = productDate;
    }
    
    public DateTime ProductDate { get; }
} 