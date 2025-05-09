using InventryOrderManagementSystem.DAL.Models;

namespace InventryOrderManagementSystem.DAL.RepositoryInterfaces
{
    public interface IOrderRepository
    {
        Task<bool> AddorderAsync(Order order);
        Task<Order?> GetOrderByIdAsync(Guid id);
    }
}
