using InventryOrderManagementSystem.DAL.Data;
using InventryOrderManagementSystem.DAL.Models;
using InventryOrderManagementSystem.DAL.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace InventryOrderManagementSystem.DAL.Repositories
{
    public class OrderRepository(AppDbContext dbContext) : IOrderRepository
    {
        public async Task<Order> AddorderAsync(Order order)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
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
                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return order;
            }
            catch (Exception ex) {
                await transaction.RollbackAsync();
                throw ex;
            }
        }
        public async Task<Order?> GetOrderByIdAsync(Guid id)
        {
            return await dbContext.Orders.FindAsync(id);
        }
        public async Task GetOrdersNotCompletedInThreeDAys(DateTime CutoffDate)
        {
            var oldPendingOrders = await dbContext.Orders
                                .Where(o => o.Status == OrderStatus.Pending && o.CreatedAt <= CutoffDate)
                                .ToListAsync();
            if (oldPendingOrders.Any())
            {
                foreach (var order in oldPendingOrders)
                {
                    order.Status = OrderStatus.Cancelled;
                }

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
