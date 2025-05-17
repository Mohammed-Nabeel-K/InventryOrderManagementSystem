namespace InventryOrderManagementSystem.DAL.Models.Common
{
    public class SaleData
    {
        public Guid OrderId { get; set; }
        public Guid OrderItemId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int QuantitySold { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total {  get; set; }
        public DateTime SaleDate { get; set; }
    }
}
