using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaktiBg.Infrastructure.Migrations
{
    public partial class EventTypeConnectionsSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "EventTypeConnections",
                columns: new[] { "EventId", "EventTypeId" },
                values: new object[,]
                {
                    { 50, 1 },
                    { 50, 3 },
                    { 50, 12 },
                    { 51, 1 },
                    { 51, 3 },
                    { 51, 10 },
                    { 52, 8 }
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EventTypeConnections",
                keyColumns: new[] { "EventId", "EventTypeId" },
                keyValues: new object[] { 50, 1 });

            migrationBuilder.DeleteData(
                table: "EventTypeConnections",
                keyColumns: new[] { "EventId", "EventTypeId" },
                keyValues: new object[] { 50, 3 });

            migrationBuilder.DeleteData(
                table: "EventTypeConnections",
                keyColumns: new[] { "EventId", "EventTypeId" },
                keyValues: new object[] { 50, 12 });

            migrationBuilder.DeleteData(
                table: "EventTypeConnections",
                keyColumns: new[] { "EventId", "EventTypeId" },
                keyValues: new object[] { 51, 1 });

            migrationBuilder.DeleteData(
                table: "EventTypeConnections",
                keyColumns: new[] { "EventId", "EventTypeId" },
                keyValues: new object[] { 51, 3 });

            migrationBuilder.DeleteData(
                table: "EventTypeConnections",
                keyColumns: new[] { "EventId", "EventTypeId" },
                keyValues: new object[] { 51, 10 });

            migrationBuilder.DeleteData(
                table: "EventTypeConnections",
                keyColumns: new[] { "EventId", "EventTypeId" },
                keyValues: new object[] { 52, 8 });

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

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 50,
                column: "CreationDate",
                value: new DateTime(2024, 4, 11, 18, 15, 27, 112, DateTimeKind.Local).AddTicks(5025));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 51,
                column: "CreationDate",
                value: new DateTime(2024, 4, 11, 18, 15, 27, 112, DateTimeKind.Local).AddTicks(5037));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 52,
                column: "CreationDate",
                value: new DateTime(2024, 4, 11, 18, 15, 27, 112, DateTimeKind.Local).AddTicks(5043));
        }
    }
}
