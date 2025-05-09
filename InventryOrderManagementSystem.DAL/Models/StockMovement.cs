namespace InventryOrderManagementSystem.DAL.Models
{
    public class StockMovement
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Change { get; set; }
        public ReasonForStockMovement Reason { get; set; }
        public Guid PerformedBy { get; set; }
        public User PerformedByUser { get; set; }
        public DateTime Timstamp { get; set; }
    }

    public enum ReasonForStockMovement
    {
        Order,
        Restock,
        Return
    }

}
