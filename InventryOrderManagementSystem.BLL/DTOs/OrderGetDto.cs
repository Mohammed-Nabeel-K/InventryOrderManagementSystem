using InventryOrderManagementSystem.DAL.Models;

namespace InventryOrderManagementSystem.BLL.DTOs
{
    public class OrderGetDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime OrderDate { get; set; }
        public Decimal TotelAmount { get; set; }
        public OrderStatus Status { get; set; }
        public ICollection<OrderItem> Items { get; set; }
    }
}
