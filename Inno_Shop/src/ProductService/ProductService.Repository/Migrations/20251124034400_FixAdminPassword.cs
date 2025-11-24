using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductService.Repository.Migrations
{
    /// <inheritdoc />
    public partial class FixAdminPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("4145bbe3-5e1e-4034-9d54-da63f56c02c3"),
                columns: new[] { "DateOfCreation", "DateOfLastUpdate" },
                values: new object[] { new DateTime(2025, 11, 23, 12, 23, 42, 798, DateTimeKind.Utc).AddTicks(9981), new DateTime(2025, 11, 23, 12, 23, 42, 798, DateTimeKind.Utc).AddTicks(9982) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("560a5138-59b7-43be-aac4-03056c4a27d8"),
                columns: new[] { "DateOfCreation", "DateOfLastUpdate" },
                values: new object[] { new DateTime(2025, 11, 23, 12, 23, 42, 798, DateTimeKind.Utc).AddTicks(9988), new DateTime(2025, 11, 23, 12, 23, 42, 798, DateTimeKind.Utc).AddTicks(9988) });
        }
    }
}
