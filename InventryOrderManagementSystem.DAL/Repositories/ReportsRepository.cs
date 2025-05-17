using InventryOrderManagementSystem.DAL.Data;
using InventryOrderManagementSystem.DAL.Models;
using InventryOrderManagementSystem.DAL.Models.Common;
using InventryOrderManagementSystem.DAL.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace InventryOrderManagementSystem.DAL.Repositories
{
    public class ReportsRepository(AppDbContext dbContext) : IReportsRepository
    {

        public async Task<List<OrderItem>> GetSalesReportByDate(DateTime From,DateTime To)
        {
            return await dbContext.OrderItems
                .Include(o => o.Product)
                .Include(o => o.Order)
                .Where(o => o.Order.CreatedAt >= From && o.Order.CreatedAt <= To)
                .ToListAsync();
        }
        public async Task<UserSalesData> GetSalesByUserId(Guid userId,DateOnly Month)
        {
            var topProducts = await dbContext.OrderItems
                .Where(oi => oi.Order.CreatedBy == userId && oi.Order.CreatedAt.Month == Month.Month && oi.Order.CreatedAt.Year == Month.Year)
                .GroupBy(oi => new { oi.ProductId, oi.Product })
                .OrderByDescending(g => g.Sum(oi => oi.Quantity))
                .Take(5)
                .Select(p => new TopProduct
                {
                    ProductId = p.Key.ProductId,
                    Name = p.Key.Product.Name,
                    QuantitySold = p.Sum(oi => oi.Quantity)
                }).ToListAsync();
            var sales = await dbContext.OrderItems
                .Include(o => o.Product)
                .Include(o => o.Order)
                .ThenInclude(o => o.CreatedByUser)
                .Where(oi => oi.Order.CreatedBy == userId && oi.Order.CreatedAt.Month == Month.Month && oi.Order.CreatedAt.Year == Month.Year)
                .ToListAsync();
            var totalSales = sales.Sum(s => s.Quantity);
            var totalRevenue = sales.Sum(s => s.Quantity * s.Product.Price);
            return new UserSalesData
            {
                UserId = userId,
                UserName = sales.FirstOrDefault()?.Order.CreatedByUser.Name,
                TotalOrders = totalSales,
                TotalRevenue = totalRevenue,
                TopProductsSold = topProducts,
            };
        }
    }
}
