using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaktiBg.Infrastructure.Migrations
{
    public partial class TypeIdDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Events");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
