using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductService.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("4145bbe3-5e1e-4034-9d54-da63f56c02c3"),
                columns: new[] { "DateOfCreation", "DateOfLastUpdate" },
                values: new object[] { new DateTime(2025, 11, 30, 1, 22, 6, 517, DateTimeKind.Utc).AddTicks(6915), new DateTime(2025, 11, 30, 1, 22, 6, 517, DateTimeKind.Utc).AddTicks(6917) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("560a5138-59b7-43be-aac4-03056c4a27d8"),
                columns: new[] { "DateOfCreation", "DateOfLastUpdate" },
                values: new object[] { new DateTime(2025, 11, 30, 1, 22, 6, 517, DateTimeKind.Utc).AddTicks(6929), new DateTime(2025, 11, 30, 1, 22, 6, 517, DateTimeKind.Utc).AddTicks(6929) });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId",
                table: "Products",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_Name",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UserId",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("4145bbe3-5e1e-4034-9d54-da63f56c02c3"),
                columns: new[] { "DateOfCreation", "DateOfLastUpdate" },
                values: new object[] { new DateTime(2025, 11, 24, 3, 44, 0, 667, DateTimeKind.Utc).AddTicks(6541), new DateTime(2025, 11, 24, 3, 44, 0, 667, DateTimeKind.Utc).AddTicks(6541) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("560a5138-59b7-43be-aac4-03056c4a27d8"),
                columns: new[] { "DateOfCreation", "DateOfLastUpdate" },
                values: new object[] { new DateTime(2025, 11, 24, 3, 44, 0, 667, DateTimeKind.Utc).AddTicks(6550), new DateTime(2025, 11, 24, 3, 44, 0, 667, DateTimeKind.Utc).AddTicks(6551) });
        }
    }
}
