using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductService.Repository.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("4145bbe3-5e1e-4034-9d54-da63f56c02c3"),
                columns: new[] { "DateOfCreation", "DateOfLastUpdate" },
                values: new object[] { new DateTime(2025, 11, 30, 1, 22, 6, 517, DateTimeKind.Utc).AddTicks(6910), new DateTime(2025, 11, 30, 1, 22, 6, 517, DateTimeKind.Utc).AddTicks(6910) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("560a5138-59b7-43be-aac4-03056c4a27d8"),
                columns: new[] { "DateOfCreation", "DateOfLastUpdate" },
                values: new object[] { new DateTime(2025, 11, 30, 1, 22, 6, 517, DateTimeKind.Utc).AddTicks(6920), new DateTime(2025, 11, 30, 1, 22, 6, 517, DateTimeKind.Utc).AddTicks(6920) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "DateOfCreation", "DateOfLastUpdate", "Description", "ImageUrl", "IsActive", "IsDeleted", "Location", "Name", "Price", "UserId" },
                values: new object[,]
                {
                    { new Guid("3e03a29f-7328-46be-8475-3d0c93e1d29c"), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new DateTime(2025, 12, 1, 3, 4, 47, 796, DateTimeKind.Utc).AddTicks(7900), new DateTime(2025, 12, 1, 3, 4, 47, 796, DateTimeKind.Utc).AddTicks(7870), "Легкий мощный ноутбук", null, true, true, null, "MacBook Air", 1200m, new Guid("f2b8dd4e-bb69-41ce-8fd6-f213790a460b") },
                    { new Guid("5dae7ef3-f812-457c-b8a4-49cfa9d9fd2a"), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new DateTime(2025, 12, 1, 21, 22, 4, 997, DateTimeKind.Utc).AddTicks(1010), new DateTime(2025, 12, 1, 21, 22, 4, 997, DateTimeKind.Utc).AddTicks(980), "Два прекрасных экрана по цене одного. AMOLED даёт чёткое, яркое и насыщенное изображение, а E-Ink экономит заряд вашего телефона и позволяет с комфортом читать с него при ярком солнечном свете.", null, true, false, null, "Yotaphone 2", 110m, new Guid("80abbca8-664d-4b20-b5de-024705497d4a") },
                    { new Guid("6a1a626b-8b48-42c5-8107-99a2a0e517fe"), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new DateTime(2025, 11, 29, 19, 36, 23, 966, DateTimeKind.Utc).AddTicks(2820), new DateTime(2025, 11, 29, 19, 36, 23, 966, DateTimeKind.Utc).AddTicks(2800), "Old low budget notebook", null, true, false, null, "Samsung RV513", 100m, new Guid("80abbca8-664d-4b20-b5de-024705497d4a") },
                    { new Guid("76612c72-c934-4a97-8195-78a85badb03e"), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new DateTime(2025, 11, 24, 4, 57, 27, 431, DateTimeKind.Utc).AddTicks(7010), new DateTime(2025, 11, 24, 4, 57, 27, 431, DateTimeKind.Utc).AddTicks(6980), "Test desc", null, true, true, null, "Test Phone", 100m, new Guid("f2b8dd4e-bb69-41ce-8fd6-f213790a460b") },
                    { new Guid("8b007a4c-b9d0-4508-b233-ba47ffeb185f"), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new DateTime(2025, 11, 29, 19, 34, 28, 328, DateTimeKind.Utc).AddTicks(9860), new DateTime(2025, 11, 29, 19, 34, 28, 328, DateTimeKind.Utc).AddTicks(9830), "Modern cut edge powerfull notebook", null, true, false, null, "Asus K501-UW", 1000m, new Guid("80abbca8-664d-4b20-b5de-024705497d4a") },
                    { new Guid("b3774de9-7aa9-447a-91e0-68bad89813d8"), new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), new DateTime(2025, 12, 7, 21, 8, 41, 834, DateTimeKind.Utc).AddTicks(4470), new DateTime(2025, 12, 7, 21, 8, 41, 834, DateTimeKind.Utc).AddTicks(4450), "Классика жанра сай-фай. По этому произведению был снят одноименный фильм. А законы робототехники вот-вот найдут применение.", null, true, false, null, "Я робот - Айзек Азимов", 49m, new Guid("80abbca8-664d-4b20-b5de-024705497d4a") },
                    { new Guid("d13254ef-108e-4a22-9c15-f70e7479898c"), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new DateTime(2025, 12, 7, 21, 29, 11, 387, DateTimeKind.Utc).AddTicks(5360), new DateTime(2025, 12, 7, 21, 29, 11, 387, DateTimeKind.Utc).AddTicks(5350), "Электронный гаджет на основе технологии E-Ink. Предназначен для комфортного чтения электронных книг. У него экономичный режим энергопотребления, которого хватает на неделю работы. Возьми с собой всего Гарри Поттера!", null, true, false, null, "Kindle E-Ink Book", 350m, new Guid("80abbca8-664d-4b20-b5de-024705497d4a") },
                    { new Guid("f8e433ff-f6db-4cf5-aa29-a1db7fd9bd18"), new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), new DateTime(2025, 12, 1, 0, 50, 48, 229, DateTimeKind.Utc).AddTicks(1300), new DateTime(2025, 12, 1, 0, 50, 48, 229, DateTimeKind.Utc).AddTicks(1280), "Прекрасная автобиографическая книга о солнечном детстве писателя на греческом острове Корфу. Одна из трилогии.", null, true, false, null, "Сад богов - Джеральд Даррелл", 150m, new Guid("80abbca8-664d-4b20-b5de-024705497d4a") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3e03a29f-7328-46be-8475-3d0c93e1d29c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5dae7ef3-f812-457c-b8a4-49cfa9d9fd2a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("6a1a626b-8b48-42c5-8107-99a2a0e517fe"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("76612c72-c934-4a97-8195-78a85badb03e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8b007a4c-b9d0-4508-b233-ba47ffeb185f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b3774de9-7aa9-447a-91e0-68bad89813d8"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d13254ef-108e-4a22-9c15-f70e7479898c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f8e433ff-f6db-4cf5-aa29-a1db7fd9bd18"));

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
        }
    }
}
