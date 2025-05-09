using InventryOrderManagementSystem.DAL.Data;
using InventryOrderManagementSystem.DAL.Models;
using InventryOrderManagementSystem.DAL.RepositoryInterfaces;

namespace InventryOrderManagementSystem.DAL.Repositories
{
    public class OrderRepository(AppDbContext dbContext) : IOrderRepository
    {
        public async Task<bool> AddorderAsync(Order order)
        {
            await dbContext.AddAsync(order);
            foreach (var item in order.Items)
            {
                var product = await dbContext.Products.FindAsync(item.ProductId);
                if (product != null)
                {
                    product.QuantityInStock -= item.Quantity;
                    await dbContext.StockMovements.AddAsync(new StockMovement
                    {
                        ProductId = product.Id,
                        Change = -item.Quantity,
                        Reason = ReasonForStockMovement.Order,
                        PerformedBy = order.CreatedBy,
                        Timstamp = DateTime.UtcNow
                    });
                }
                
            }
            return await dbContext.SaveChangesAsync()>0;
        }
        public async Task<Order?> GetOrderByIdAsync(Guid id)
        {
            return await dbContext.Orders.FindAsync(id);
        }
    }
}
