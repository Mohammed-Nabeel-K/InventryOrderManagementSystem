using InventryOrderManagementSystem.DAL.Models;

namespace InventryOrderManagementSystem.DAL.RepositoryInterfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> ProductById(Guid id);
        Task<bool> AddStockAsync(StockMovement stockMovement);
    }
}
