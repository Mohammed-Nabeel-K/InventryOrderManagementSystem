using InventryOrderManagementSystem.DAL.Models;

namespace InventryOrderManagementSystem.DAL.RepositoryInterfaces
{
    public interface IOrderRepository
    {
        Task<Order> AddorderAsync(Order order);
        Task<Order?> GetOrderByIdAsync(Guid id);
        Task GetOrdersNotCompletedInThreeDAys(DateTime CutoffDate);
    }
}
