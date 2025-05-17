using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventryOrderManagementSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SecondCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ReorderLevel",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "Email", "Name", "Phone" },
                values: new object[] { new Guid("34d0a13e-b225-4ae0-9903-95fcf73cd053"), "123 Main St, Cityville", "johndoe@gmail.com", "John Doe", "1234567890" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "LastUpdatedAt", "Name", "Price", "QuantityInStock", "ReorderLevel", "SKU", "UpdatedBy" },
                values: new object[] { new Guid("34d0a13e-b225-4ae0-9903-95fcf73cd052"), new DateTime(2025, 5, 10, 14, 22, 41, 820, DateTimeKind.Local), new Guid("6bef721a-3010-45d6-b7b9-0508951eb776"), true, new DateTime(2025, 5, 10, 14, 22, 41, 820, DateTimeKind.Local), "Sample Product", 10.99m, 100, 10, "SP001", new Guid("6bef721a-3010-45d6-b7b9-0508951eb776") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("34d0a13e-b225-4ae0-9903-95fcf73cd053"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("34d0a13e-b225-4ae0-9903-95fcf73cd052"));

            migrationBuilder.AlterColumn<string>(
                name: "ReorderLevel",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
