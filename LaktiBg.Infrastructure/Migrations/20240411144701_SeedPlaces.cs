using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaktiBg.Infrastructure.Migrations
{
    public partial class SeedPlaces : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "539e62e9-7926-446b-8d9c-92cd370dfde8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b6518a98-95ad-44d4-8ac0-22b68cc2bae8", "AQAAAAEAACcQAAAAEOjxkEzj7RMv5VxvoWhyhYXCYo8NdT10SfDyT3+Veqy3mBkVKe7VnJ6ITcMeTqmo2g==", "30758010-ae5b-455f-afd4-4dd6951cc4c8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "71368c9b-91fa-4338-bfce-e0921b5324ef",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b43e57cb-1558-4651-85a0-4e2d78ee7687", "AQAAAAEAACcQAAAAEN/y02jE0Lio/J2ayOK3idwWd97mQhQVi6N3r85kCz2unYsgOZrB74NcuXKR3hY6Bw==", "7e915324-9bc6-4528-93de-643d8fc2ef7f" });

            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "Id", "Address", "Contact", "IsApproved", "IsPublic", "Name", "OwnerId", "Rating" },
                values: new object[,]
                {
                    { 42, "ул. „Златю Бояджиев“ 2, 4000 Пловдив", "0700 20 888", true, true, "Happy Bar & Grill", "539e62e9-7926-446b-8d9c-92cd370dfde8", 5m },
                    { 43, "Свинова поляна, 5641, град Априлци", "+359878655666", true, true, "Вила Петра", "71368c9b-91fa-4338-bfce-e0921b5324ef", 5m },
                    { 44, "Западна промишлена зонаЗападен, ул. „Перущица“ 8, 4002 Пловдив", "032 273 000", true, true, "Cinema City Пловдив", "71368c9b-91fa-4338-bfce-e0921b5324ef", 5m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "539e62e9-7926-446b-8d9c-92cd370dfde8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4a78ed9c-a649-4b91-ac24-451e7aedd2b2", "AQAAAAEAACcQAAAAEIQ4UKG0oWihsCFpkY70fQ6dVdvk6jhoM74nphAoSRBtOc/osr2HX3I511+pcGpnIQ==", "fa04520e-0955-4291-8d38-6536b265d2e4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "71368c9b-91fa-4338-bfce-e0921b5324ef",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d7456a3d-4a16-4134-9747-dec2bb1a5cf8", "AQAAAAEAACcQAAAAEMJ+gCn3KsbqJGs/giqw3220+lR5SqzAmnxa6H4HVk81AmQlwf9bzaYp5XdG78OJTQ==", "fbb491b2-879f-40ba-b72c-88ce79902bb0" });
        }
    }
}
