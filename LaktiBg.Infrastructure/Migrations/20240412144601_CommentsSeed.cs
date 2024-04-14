using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaktiBg.Infrastructure.Migrations
{
    public partial class CommentsSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "AuthorId", "DateTime", "EventId", "Text" },
                values: new object[,]
                {
                    { 42, "539e62e9-7926-446b-8d9c-92cd370dfde8", new DateTime(2024, 4, 11, 14, 22, 0, 0, DateTimeKind.Unspecified), 50, "Излезе ли новото меню?" },
                    { 43, "71368c9b-91fa-4338-bfce-e0921b5324ef", new DateTime(2024, 4, 11, 17, 45, 0, 0, DateTimeKind.Unspecified), 50, "Да, много е добро!" },
                    { 44, "539e62e9-7926-446b-8d9c-92cd370dfde8", new DateTime(2024, 4, 11, 20, 1, 0, 0, DateTimeKind.Unspecified), 50, "Супер! Ще се видим там" },
                    { 45, "539e62e9-7926-446b-8d9c-92cd370dfde8", new DateTime(2024, 4, 12, 20, 22, 0, 0, DateTimeKind.Unspecified), 51, "Къщата има ли басейн?" },
                    { 46, "71368c9b-91fa-4338-bfce-e0921b5324ef", new DateTime(2024, 4, 12, 22, 10, 24, 0, DateTimeKind.Unspecified), 51, "Не, в съседната къща има и може да се ползва, тъй като е на същите собственици." },
                    { 47, "71368c9b-91fa-4338-bfce-e0921b5324ef", new DateTime(2024, 4, 12, 10, 10, 24, 0, DateTimeKind.Unspecified), 52, "Ще закъснея малко." }
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "539e62e9-7926-446b-8d9c-92cd370dfde8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "35e8efc2-abef-4c2e-985c-f4ff68b2b769", "AQAAAAEAACcQAAAAEF0f1glRNOSCYwcQA3n6zbciM7dU+qyAPzxuAPLgYaG3yCKOUwwOFyejFgSyEc5WTw==", "5ccf3382-faa7-42eb-8b29-14b970ac064d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "71368c9b-91fa-4338-bfce-e0921b5324ef",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c5287ef3-e636-4648-aa78-c2cc61cfff34", "AQAAAAEAACcQAAAAENtlnZR20qgO0fvI4BKzI4yzdKapblrTiva2CB5alYW6umelmaAvjjkleHT575uTmQ==", "477e57f5-f49e-49d7-a80f-528f7841af99" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 50,
                column: "CreationDate",
                value: new DateTime(2024, 4, 12, 17, 29, 17, 921, DateTimeKind.Local).AddTicks(7402));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 51,
                column: "CreationDate",
                value: new DateTime(2024, 4, 12, 17, 29, 17, 921, DateTimeKind.Local).AddTicks(7412));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 52,
                column: "CreationDate",
                value: new DateTime(2024, 4, 12, 17, 29, 17, 921, DateTimeKind.Local).AddTicks(7418));
        }
    }
}
