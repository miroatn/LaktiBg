using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaktiBg.Infrastructure.Migrations
{
    public partial class EventSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "539e62e9-7926-446b-8d9c-92cd370dfde8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f259dd32-c012-4c87-ba06-71cf4a5d6232", "AQAAAAEAACcQAAAAENBRi1eJh/K7KDTpRsk9AEVHmY1kCTv7u6rAL74wUpV4UH0hsdnJur8wQIitYTMi3Q==", "3882408b-07a8-4f0e-9daf-c4b6cce13635" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "71368c9b-91fa-4338-bfce-e0921b5324ef",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "95f3ef1f-0303-4eab-85ba-cf0d41a0a61b", "AQAAAAEAACcQAAAAEOpsbb94bgERqtjp9BaD2HxvGxN3yAgHaNOIQeGFsNbvWF6Jk3leKglaStszulEb9Q==", "d6d3e982-1011-43ff-bd46-b2ec828b9f95" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "CreationDate", "Description", "IsDeleted", "IsFinished", "IsPublic", "IsVisible", "MinAgeRequired", "MinRatingRequired", "Name", "OrganizerId", "ParticipantsMaxCount", "PlaceId", "StartDate" },
                values: new object[,]
                {
                    { 50, new DateTime(2024, 4, 11, 18, 15, 27, 112, DateTimeKind.Local).AddTicks(5025), "Смятам да почерпя по случай взетия изпит, не приемам не за отговор!", false, false, true, true, null, 3, "Хапване на Хепи", "71368c9b-91fa-4338-bfce-e0921b5324ef", 10, 42, new DateTime(2024, 5, 1, 20, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 51, new DateTime(2024, 4, 11, 18, 15, 27, 112, DateTimeKind.Local).AddTicks(5037), "Пет рожденника ще почерпим за рожденните дни, партито започва в 2 на обяд в петък и приключва в неделя. Нощувките се поемат от рождениците", false, false, true, true, null, 3, "Събиране по случай петорния рожден ден", "71368c9b-91fa-4338-bfce-e0921b5324ef", 29, 43, new DateTime(2024, 11, 8, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 52, new DateTime(2024, 4, 11, 18, 15, 27, 112, DateTimeKind.Local).AddTicks(5043), "Ще ходим до Пловдив да гледаме Дюн 2 в Cinema city с моята кола.", false, true, true, true, null, 5, "Дюн 2", "539e62e9-7926-446b-8d9c-92cd370dfde8", 5, 44, new DateTime(2024, 3, 28, 20, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 52);

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
        }
    }
}
