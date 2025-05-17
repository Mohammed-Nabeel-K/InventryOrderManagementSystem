using InventryOrderManagementSystem.DAL.Models;

namespace InventryOrderManagementSystem.DAL.RepositoryInterfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> ProductById(Guid id);
        Task<bool> IsProductExists(Guid productId);
        Task<bool> UpdateStockAsync(StockMovement stockMovement);
        Task<Product> CreateProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<List<Product>> GetLowStockProducts();
    }
}
