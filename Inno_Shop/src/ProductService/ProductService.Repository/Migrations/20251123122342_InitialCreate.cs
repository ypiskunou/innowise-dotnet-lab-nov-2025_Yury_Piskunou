using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductService.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                    Description = table.Column<string>(type: "text", nullable: true)
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
                    DateOfCreation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateOfLastUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    Location = table.Column<string>(type: "text", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false)
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
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Printed and electronic books", "Books" },
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Gadgets and electronic devices", "Electronics" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "DateOfCreation", "DateOfLastUpdate", "Description", "ImageUrl", "IsActive", "IsDeleted", "Location", "Name", "Price", "UserId" },
                values: new object[,]
                {
                    { new Guid("4145bbe3-5e1e-4034-9d54-da63f56c02c3"), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new DateTime(2025, 11, 23, 12, 23, 42, 798, DateTimeKind.Utc).AddTicks(9981), new DateTime(2025, 11, 23, 12, 23, 42, 798, DateTimeKind.Utc).AddTicks(9982), "A very powerful and modern laptop.", null, true, false, null, "Awesome Laptop", 1299.99m, new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a") },
                    { new Guid("560a5138-59b7-43be-aac4-03056c4a27d8"), new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), new DateTime(2025, 11, 23, 12, 23, 42, 798, DateTimeKind.Utc).AddTicks(9988), new DateTime(2025, 11, 23, 12, 23, 42, 798, DateTimeKind.Utc).AddTicks(9988), "A must-read for all .NET developers.", null, true, false, null, "Ultimate Web API Book", 49.99m, new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a") }
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
