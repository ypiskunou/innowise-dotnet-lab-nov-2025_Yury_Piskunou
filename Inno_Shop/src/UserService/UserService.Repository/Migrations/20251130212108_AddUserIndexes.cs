using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserService.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                columns: new[] { "PasswordHash", "VerifiedAt" },
                values: new object[] { "AQAAAAIAAYagAAAAEGtVOXkmwwc57KzDGVK1Y8M8GCICFLFvDpaDilkYvr48jPPbt1vOxyTeCUCD0s6p+Q==", new DateTime(2025, 11, 30, 21, 21, 7, 762, DateTimeKind.Utc).AddTicks(1820) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateOfBirth", "Email", "IsActive", "Name", "PasswordHash", "PhoneNumber", "VerificationToken", "VerifiedAt" },
                values: new object[,]
                {
                    { new Guid("84fe1970-e510-45c3-90a8-8e0607d056e9"), null, "user@example.com", true, "Regular User", "AQAAAAIAAYagAAAAEHP8SZ2ACcbxZChtJd4YrJW3H1hfZYbUpWAeifxw6heZoZpI9PJcs+Ks5S0ul4ryCQ==", null, null, new DateTime(2025, 11, 30, 21, 21, 7, 762, DateTimeKind.Utc).AddTicks(2063) },
                    { new Guid("f2b8dd4e-bb69-41ce-8fd6-f213790a460b"), null, "volchkov@inno.by", false, "Anton Volchkov", "AQAAAAIAAYagAAAAEGzGIA5pP3SUaaSHYI093ON4tjYMcLdcY3MDAXB0CwtQIENCmU8jSxEqRfc4ze8XUg==", null, null, new DateTime(2025, 11, 30, 21, 21, 7, 762, DateTimeKind.Utc).AddTicks(2068) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Name",
                table: "Users",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Name",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("84fe1970-e510-45c3-90a8-8e0607d056e9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f2b8dd4e-bb69-41ce-8fd6-f213790a460b"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                columns: new[] { "PasswordHash", "VerifiedAt" },
                values: new object[] { "HASHED_ADMIN_PASSWORD", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateOfBirth", "Email", "IsActive", "Name", "PasswordHash", "PhoneNumber", "VerificationToken", "VerifiedAt" },
                values: new object[] { new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), null, "user@example.com", true, "Regular User", "HASHED_USER_PASSWORD", null, null, null });
        }
    }
}
