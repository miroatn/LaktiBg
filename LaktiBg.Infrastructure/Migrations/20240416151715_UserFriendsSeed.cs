using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaktiBg.Infrastructure.Migrations
{
    public partial class UserFriendsSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "539e62e9-7926-446b-8d9c-92cd370dfde8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "547e3499-6bfe-4ff7-aefa-42ef8f59257e", "AQAAAAEAACcQAAAAEEnPtnZd85pz7OFINsZCK7xca6cO+3LPfmAcXB7n0JMOx1MI3u9cVbl2hPVRQ8KvFQ==", "ee026ef8-1ae8-49b8-9dec-88e558c016e3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "71368c9b-91fa-4338-bfce-e0921b5324ef",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f28b09e6-1b62-4454-ab75-a5bac5518a63", "AQAAAAEAACcQAAAAEOYjI3Fns9um5CqgQkp74LwAR82ExTQfeZ77c3hTQCujfziwgYjRNNX46IEEP61Ymw==", "fcfbf107-f19c-49f2-8f32-0055c1ef9441" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 50,
                column: "CreationDate",
                value: new DateTime(2024, 4, 16, 18, 17, 14, 722, DateTimeKind.Local).AddTicks(8061));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 51,
                column: "CreationDate",
                value: new DateTime(2024, 4, 16, 18, 17, 14, 722, DateTimeKind.Local).AddTicks(8071));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 52,
                column: "CreationDate",
                value: new DateTime(2024, 4, 16, 18, 17, 14, 722, DateTimeKind.Local).AddTicks(8078));

            migrationBuilder.InsertData(
                table: "UserFriends",
                columns: new[] { "Id", "IsAccepted", "UserFriendId", "UserId", "VisitedEventCounter" },
                values: new object[,]
                {
                    { 1, true, "71368c9b-91fa-4338-bfce-e0921b5324ef", "539e62e9-7926-446b-8d9c-92cd370dfde8", 0 },
                    { 2, true, "539e62e9-7926-446b-8d9c-92cd370dfde8", "71368c9b-91fa-4338-bfce-e0921b5324ef", 0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserFriends",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserFriends",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "539e62e9-7926-446b-8d9c-92cd370dfde8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6eafaf13-1106-47da-a0d2-fe4a3f973778", "AQAAAAEAACcQAAAAEHEfKYrrdGJcqHfYiBs5HqN3zdXWTr4kNvL6FkJc/xdXZaWMrUAnoH4QNBcUm0+d0g==", "4efdff15-57ac-4fa0-b34e-a30a4db6c1d4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "71368c9b-91fa-4338-bfce-e0921b5324ef",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a055362f-41ad-4991-90ba-39039c3abd79", "AQAAAAEAACcQAAAAEDtV25ZW+wK8VXIjTEsnLe5VHuFbRa4Um8xrmyS9DnN0Tfh+9PUrCg2D8r43fb8g6Q==", "23c35a8b-ad3b-4b9e-8b4f-a68bacd1c1cf" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 50,
                column: "CreationDate",
                value: new DateTime(2024, 4, 12, 17, 46, 0, 589, DateTimeKind.Local).AddTicks(4197));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 51,
                column: "CreationDate",
                value: new DateTime(2024, 4, 12, 17, 46, 0, 589, DateTimeKind.Local).AddTicks(4207));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 52,
                column: "CreationDate",
                value: new DateTime(2024, 4, 12, 17, 46, 0, 589, DateTimeKind.Local).AddTicks(4213));
        }
    }
}
