using InventryOrderManagementSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventryOrderManagementSystem.DAL.Data.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(new User
            {
                Id = Guid.Parse("34d0a13e-b225-4ae0-9903-95fcf73cd051"),
                Name = "Admin",
                Email = "admin@inv.com",
                Role = UserRoles.Admin,
                IsActive = false,
                PasswordHashed = "$2a$11$IPysT70RC9hzmhMCwCcuVOVYpQ1TrfL4nv5KjAE3sFF3IMxOxLJa2",
                CreatedAt = DateTime.Parse("2025-05-09T08:52:41.820Z"),
                LastUpdatedAt = DateTime.Parse("2025-05-09T08:52:41.820Z")
            },new User
            {
                Id = Guid.Parse("6bef721a-3010-45d6-b7b9-0508951eb776"),
                Name = "Manager",
                Email = "manager@inv.com",
                Role = UserRoles.Manager,
                IsActive = false,
                PasswordHashed = "$2a$11$m3yCb45fte8Uxe5.idqR4uimJ25RqRAkwGKRNay2m0tUXZe27aFAW",
                CreatedAt = DateTime.Parse("2025-05-09T08:52:41.820Z"),
                LastUpdatedAt = DateTime.Parse("2025-05-09T08:52:41.820Z")
            },new User
            {
                Id = Guid.Parse("7d415d48-cda3-4231-9b2b-625af21af39f"),
                Name = "Sales",
                Email = "sales@inv.com",
                Role = UserRoles.Sales,
                IsActive = false,
                PasswordHashed = "$2a$11$gBVtVNCSb1za7roFSqiBFul4Cbkbf0l130cC/2Pfr.aJonQ.h6ICy",
                CreatedAt = DateTime.Parse("2025-05-09T08:52:41.820Z"),
                LastUpdatedAt = DateTime.Parse("2025-05-09T08:52:41.820Z")
            });
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(u => u.PasswordHashed).IsRequired();
            builder.Property(u => u.Role)
                .IsRequired()
                .HasConversion<string>();
            builder.Property(u => u.CreatedAt).IsRequired();
            builder.Property(u => u.LastUpdatedAt).IsRequired();
            builder.HasIndex(u => u.Email)
                .IsUnique()
                .HasDatabaseName("IX_User_Email");
        }
    }
}
