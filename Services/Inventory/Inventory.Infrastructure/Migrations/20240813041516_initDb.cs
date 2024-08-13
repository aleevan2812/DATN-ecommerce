using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Inventory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Seller = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Author = table.Column<string>(type: "text", nullable: false),
                    ISBN = table.Column<string>(type: "text", nullable: false),
                    ListPrice = table.Column<double>(type: "double precision", nullable: true),
                    Price = table.Column<double>(type: "double precision", nullable: true),
                    Price50 = table.Column<double>(type: "double precision", nullable: true),
                    Price100 = table.Column<double>(type: "double precision", nullable: true),
                    StockQuantity = table.Column<int>(type: "integer", nullable: false),
                    SoldQuantity = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageUrls = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { new Guid("b70e2f35-9b9b-4267-8a49-fd4670af9074"), 3, "History" },
                    { new Guid("c0a802bf-4bfc-45f0-82f7-31836d5db0d7"), 1, "Action" },
                    { new Guid("f2e8f7e4-dfcc-4ff5-a232-f2d8c8b6d6bb"), 2, "SciFi" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "CategoryId", "Description", "ISBN", "ImageUrls", "ListPrice", "Price", "Price100", "Price50", "Seller", "SoldQuantity", "StockQuantity", "Title" },
                values: new object[,]
                {
                    { new Guid("23a8bde2-cd1f-4721-bb0f-18e27fd9a945"), "Billy Spark", new Guid("c0a802bf-4bfc-45f0-82f7-31836d5db0d7"), "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", "SWD9999001", new List<string>(), 99.0, 90.0, 80.0, 85.0, "alee", 1, 10, "Fortune of Time" },
                    { new Guid("2d8bf6a5-d582-4a70-89f1-abc914cc48b9"), "Abby Muscles", new Guid("f2e8f7e4-dfcc-4ff5-a232-f2d8c8b6d6bb"), "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", "WS3333333301", new List<string>(), 70.0, 65.0, 55.0, 60.0, "rahul", 1, 10, "Cotton Candy" },
                    { new Guid("8cfab7e6-78a0-4a4f-9474-5cf7d724ee69"), "Julian Button", new Guid("c0a802bf-4bfc-45f0-82f7-31836d5db0d7"), "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", "RITO5555501", new List<string>(), 55.0, 50.0, 35.0, 40.0, "alee", 1, 10, "Vanish in the Sunset" },
                    { new Guid("c888a209-d6fb-4b36-a9d1-bcf91508d5b1"), "Ron Parker", new Guid("f2e8f7e4-dfcc-4ff5-a232-f2d8c8b6d6bb"), "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", "SOTJ1111111101", new List<string>(), 30.0, 27.0, 20.0, 25.0, "rahul", 1, 10, "Rock in the Ocean" },
                    { new Guid("ca9b37f1-5b3a-489f-8f79-8aebfa13c80f"), "Nancy Hoover", new Guid("c0a802bf-4bfc-45f0-82f7-31836d5db0d7"), "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", "CAW777777701", new List<string>(), 40.0, 30.0, 20.0, 25.0, "alee", 1, 10, "Dark Skies" },
                    { new Guid("e0b095e5-278d-4425-8e62-c108a1dbde30"), "Laura Phantom", new Guid("b70e2f35-9b9b-4267-8a49-fd4670af9074"), "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ", "FOT000000001", new List<string>(), 25.0, 23.0, 20.0, 22.0, "rahul", 1, 10, "Leaves and Wonders" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
