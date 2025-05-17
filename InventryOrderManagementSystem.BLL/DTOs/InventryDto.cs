using InventryOrderManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventryOrderManagementSystem.BLL.DTOs
{
    public class InventryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public string level { get; set; }
        public bool IsActive { get; set; }
    }
}
