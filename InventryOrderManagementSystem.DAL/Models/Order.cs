namespace InventryOrderManagementSystem.DAL.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Guid CreatedBy { get; set; }
        public User CreatedByUser { get; set; }
        public DateTime OrderDate { get; set; }
        public Decimal TotelAmount { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public ICollection<OrderItem> Items { get; set; }
    }

    public enum OrderStatus
    {
        Pending,
        Completed,
        Cancelled
    }
}
