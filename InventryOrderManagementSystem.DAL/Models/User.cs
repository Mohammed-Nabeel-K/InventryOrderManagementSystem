using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventryOrderManagementSystem.DAL.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHashed { get; set; }
        public UserRoles Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<StockMovement> StockMovements { get; set; }

    }
    public enum UserRoles
    {
        Admin,
        Manager,
        Sales
    }

}
