using InventryOrderManagementSystem.DAL.Data;
using InventryOrderManagementSystem.DAL.Models;
using InventryOrderManagementSystem.DAL.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace InventryOrderManagementSystem.DAL.Repositories
{
    public class ProductRepository(AppDbContext dbContext) : IProductRepository
    {
        public async Task<List<Product>> GetAllAsync()
        {
            return await dbContext.Products.ToListAsync();
        }
        public async Task<Product?> ProductById(Guid id)
        {
            return await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<bool> AddStockAsync(StockMovement stockMovement)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                var product = await dbContext.Products.FindAsync(stockMovement.ProductId);
                if (product == null)
                {
                    return false;
                }
                product.QuantityInStock += stockMovement.Change;
                await dbContext.StockMovements.AddAsync(stockMovement);
                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}
