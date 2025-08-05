using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Core.Entities;

public class Product
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }
    
    [Required]
    public DateTime ProductDate { get; set; }
    
    [Required]
    [StringLength(50)]
    public string CreatedBy { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    
    // Navigation property for the user who created this product
    public string UserId { get; set; } = string.Empty;
    
    // Constructor to ensure proper initialization
    public Product()
    {
        ProductDate = DateTime.UtcNow;
        CreatedAt = DateTime.UtcNow;
    }
    
    // Business logic methods
    public void UpdateProduct(string name, string description, decimal price, DateTime productDate)
    {
        Name = name;
        Description = description;
        Price = price;
        ProductDate = productDate;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public bool CanBeModifiedBy(string userId)
    {
        return UserId == userId;
    }
} 