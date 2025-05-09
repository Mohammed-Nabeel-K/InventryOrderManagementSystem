using InventryOrderManagementSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventryOrderManagementSystem.DAL.Data.Config
{
    public class StockMovementConfig : IEntityTypeConfiguration<StockMovement>
    {
        public void Configure(EntityTypeBuilder<StockMovement> builder)
        {
            builder.HasKey(sm => sm.Id);
            builder.HasOne(sm => sm.PerformedByUser)
                .WithMany(u => u.StockMovements)
                .HasForeignKey(sm => sm.PerformedBy);
            builder.HasOne(sm => sm.Product)
                .WithMany(p => p.StockMovements)
                .HasForeignKey(sm => sm.ProductId);
            builder.Property(sm => sm.Reason)
                .HasConversion<string>();
        }
    }
}
