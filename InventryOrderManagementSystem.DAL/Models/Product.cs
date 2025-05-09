namespace InventryOrderManagementSystem.DAL.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public string ReorderLevel { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public ICollection<StockMovement> StockMovements { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
