using InventryOrderManagementSystem.DAL.Models;
using InventryOrderManagementSystem.DAL.Models.Common;

namespace InventryOrderManagementSystem.DAL.RepositoryInterfaces
{
    public interface IReportsRepository
    {
        Task<List<OrderItem>> GetSalesReportByDate(DateTime From, DateTime To);
        Task<UserSalesData> GetSalesByUserId(Guid userId, DateOnly Month);
    }
}
