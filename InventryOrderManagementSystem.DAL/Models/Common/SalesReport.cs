namespace InventryOrderManagementSystem.DAL.Models.Common
{
    public class SalesReport
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public decimal TotalSales { get; set; }
        public int TotalOrders { get; set; }
        public int ItemsSold { get; set; }
        public List<TopProduct> TopProducts { get; set; }
    }

    
}
