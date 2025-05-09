using InventryOrderManagementSystem.DAL.Models;

namespace InventryOrderManagementSystem.BLL.DTOs
{
    public class OrderDto
    {
        public Guid CustomerId { get; set; }
        public ICollection<OrderItemDto> Items { get; set; }
    }
}
