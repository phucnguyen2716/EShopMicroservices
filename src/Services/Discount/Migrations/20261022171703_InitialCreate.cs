using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Discount.Grpc.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "Id", "Amount", "Description", "ProductName" },
                values: new object[,]
                {
                    { 1, 10, "Discount 10%", "iPhone 17" },
                    { 2, 15, "Discount 15%", "Samsung Galaxy S25" },
                    { 3, 20, "Discount 20%", "MacBook Air M4" },
                    { 4, 5, "Discount 5%", "iPad Air" },
                    { 5, 12, "Discount 12%", "Dell XPS 13" },
                    { 6, 8, "Discount 8%", "AirPods Pro" },
                    { 7, 18, "Discount 18%", "Sony WH-1000XM5" },
                    { 8, 10, "Discount 10%", "Apple Watch" },
                    { 9, 25, "Discount 25%", "ASUS ROG" },
                    { 10, 7, "Discount 7%", "Logitech Mouse" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coupons");
        }
    }
}
