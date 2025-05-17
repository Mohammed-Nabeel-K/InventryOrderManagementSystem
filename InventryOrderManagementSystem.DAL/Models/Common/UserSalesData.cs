using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventryOrderManagementSystem.DAL.Models.Common
{
    public class UserSalesData
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<TopProduct> TopProductsSold { get; set; }
    }
}
