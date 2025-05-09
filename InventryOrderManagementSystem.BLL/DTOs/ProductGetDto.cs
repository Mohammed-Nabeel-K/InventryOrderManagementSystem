namespace InventryOrderManagementSystem.BLL.DTOs
{
    public class ProductGetDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public string ReorderLevel { get; set; }
        public bool IsActive { get; set; }
    }
}
