using InventryOrderManagementSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventryOrderManagementSystem.DAL.Data.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder) 
        {
            builder.HasData(new Product
            {
                Id = Guid.Parse("34d0a13e-b225-4ae0-9903-95fcf73cd052"),
                Name = "Sample Product",
                SKU = "SP001",
                Price = 10.99m,
                QuantityInStock = 100,
                ReorderLevel = 10,
                IsActive = true,
                CreatedAt = DateTime.Parse("2025-05-10T08:52:41.820Z"),
                LastUpdatedAt = DateTime.Parse("2025-05-10T08:52:41.820Z"),
                CreatedBy = Guid.Parse("6bef721a-3010-45d6-b7b9-0508951eb776"),
                UpdatedBy = Guid.Parse("6bef721a-3010-45d6-b7b9-0508951eb776")
            });
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

        }
    }
}
