using InventryOrderManagementSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventryOrderManagementSystem.DAL.Data.Config
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasData(new Customer
            {
                Id = Guid.Parse("34d0a13e-b225-4ae0-9903-95fcf73cd053"),
                Name = "John Doe",
                Address = "123 Main St, Cityville",
                Email = "johndoe@gmail.com",
                Phone = "1234567890"
            });
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Email).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Address).IsRequired().HasMaxLength(200);
        }
    }
}
