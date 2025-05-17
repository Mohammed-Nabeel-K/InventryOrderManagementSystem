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
            return await dbContext.Products.SingleOrDefaultAsync(p => p.Id == id);
        }
        public async Task<bool> IsProductExists(Guid productId)
        {
            return await dbContext.Products.AnyAsync(p => p.Id == productId);
        }
        public async Task<bool> UpdateStockAsync(StockMovement stockMovement)
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
        public async Task<Product> CreateProductAsync(Product product)
        {
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return product;
        }
        public async Task<bool> UpdateProductAsync(Product product)
        {
            dbContext.Products.Update(product);
            return await dbContext.SaveChangesAsync() > 0;
        }
        public async Task<List<Product>> GetLowStockProducts()
        {
            return dbContext.Products
                    .Where(p => p.QuantityInStock < p.ReorderLevel)
                    .ToList();
        }
    }
}
