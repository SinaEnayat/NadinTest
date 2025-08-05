using ProductManagement.Core.Entities;

namespace ProductManagement.Core.Interfaces;

public interface IProductRepository
{
    // Query methods (Read operations)
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<IEnumerable<Product>> GetByUserIdAsync(string userId);
    Task<bool> ExistsByProductDateAsync(DateTime productDate);
    
    // Command methods (Create, Update, Delete operations)
    Task<Product> AddAsync(Product product);
    Task<Product> UpdateAsync(Product product);
    Task DeleteAsync(int id);
} 