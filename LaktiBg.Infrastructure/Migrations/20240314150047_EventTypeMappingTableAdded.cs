using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaktiBg.Infrastructure.Migrations
{
    public partial class EventTypeMappingTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventTypes_Events_EventId",
                table: "EventTypes");

            migrationBuilder.DropIndex(
                name: "IX_EventTypes_EventId",
                table: "EventTypes");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "EventTypes");

            migrationBuilder.CreateTable(
                name: "EventTypeConnections",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false),
                    EventTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTypeConnections", x => new { x.EventId, x.EventTypeId });
                    table.ForeignKey(
                        name: "FK_EventTypeConnections_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventTypeConnections_EventTypes_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "EventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventTypeConnections_EventTypeId",
                table: "EventTypeConnections",
                column: "EventTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventTypeConnections");

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "EventTypes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventTypes_EventId",
                table: "EventTypes",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventTypes_Events_EventId",
                table: "EventTypes",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");
        }
    }
}
