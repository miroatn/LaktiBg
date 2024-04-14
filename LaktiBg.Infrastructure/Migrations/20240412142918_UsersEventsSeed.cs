using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaktiBg.Infrastructure.Migrations
{
    public partial class UsersEventsSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "UsersEvents",
                columns: new[] { "EventId", "UserId" },
                values: new object[,]
                {
                    { 50, "539e62e9-7926-446b-8d9c-92cd370dfde8" },
                    { 50, "71368c9b-91fa-4338-bfce-e0921b5324ef" },
                    { 51, "71368c9b-91fa-4338-bfce-e0921b5324ef" },
                    { 52, "539e62e9-7926-446b-8d9c-92cd370dfde8" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UsersEvents",
                keyColumns: new[] { "EventId", "UserId" },
                keyValues: new object[] { 50, "539e62e9-7926-446b-8d9c-92cd370dfde8" });

            migrationBuilder.DeleteData(
                table: "UsersEvents",
                keyColumns: new[] { "EventId", "UserId" },
                keyValues: new object[] { 50, "71368c9b-91fa-4338-bfce-e0921b5324ef" });

            migrationBuilder.DeleteData(
                table: "UsersEvents",
                keyColumns: new[] { "EventId", "UserId" },
                keyValues: new object[] { 51, "71368c9b-91fa-4338-bfce-e0921b5324ef" });

            migrationBuilder.DeleteData(
                table: "UsersEvents",
                keyColumns: new[] { "EventId", "UserId" },
                keyValues: new object[] { 52, "539e62e9-7926-446b-8d9c-92cd370dfde8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "539e62e9-7926-446b-8d9c-92cd370dfde8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7328f55a-fce3-4990-bc67-446b563dd693", "AQAAAAEAACcQAAAAELk9wnutQUZ7hx4FO7Fwatrgs5rOIjg39D+NufwgVVRP/UUMTF2MQd0NJpRwzo09Og==", "af632eeb-d1ae-474f-9fee-c1437f5d2002" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "71368c9b-91fa-4338-bfce-e0921b5324ef",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7d6a8e9c-bcd4-4c03-9f63-2839f159e178", "AQAAAAEAACcQAAAAEGwNkPRSKIPNXfjAhjNiTSYrPs6PzOigZWuWsWnstUH0tl4fdyJ7Pj2TcPRVJbQ2Mg==", "ac12143a-a75a-4605-85d9-a0931fc32a7e" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 50,
                column: "CreationDate",
                value: new DateTime(2024, 4, 12, 17, 18, 55, 23, DateTimeKind.Local).AddTicks(1775));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 51,
                column: "CreationDate",
                value: new DateTime(2024, 4, 12, 17, 18, 55, 23, DateTimeKind.Local).AddTicks(1784));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 52,
                column: "CreationDate",
                value: new DateTime(2024, 4, 12, 17, 18, 55, 23, DateTimeKind.Local).AddTicks(1790));
        }
    }
}
