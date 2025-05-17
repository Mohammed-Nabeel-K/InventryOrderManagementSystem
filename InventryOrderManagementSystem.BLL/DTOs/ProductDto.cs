using InventryOrderManagementSystem.DAL.Models;

namespace InventryOrderManagementSystem.BLL.DTOs
{
    public class ProductDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public int ReorderLevel { get; set; }
        public bool IsActive { get; set; }
    }
}
