using InventryOrderManagementSystem.BLL.DTOs;
using InventryOrderManagementSystem.DAL.Models.Common;

namespace InventryOrderManagementSystem.BLL.SeviceInterfaces
{
    public interface IOrderServices
    {
        Task<Response<OrderDto>> AddOrderAsync(OrderDto orderDto, Guid UserId);
        Task<Response<OrderDto>> GetOrderByIdAsync(Guid id);
    }
}
