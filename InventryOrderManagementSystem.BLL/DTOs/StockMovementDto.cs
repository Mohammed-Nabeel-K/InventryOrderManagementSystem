using InventryOrderManagementSystem.DAL.Models;

namespace InventryOrderManagementSystem.BLL.DTOs
{
    public class StockMovementDto
    {
        public Guid ProductId { get; set; }
        public int Change { get; set; }
        public ReasonForStockMovement Reason { get; set; }
    }
}
