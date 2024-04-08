using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaktiBg.Infrastructure.Migrations
{
    public partial class IsApprovedPropertyAddedToPlace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Places",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "539e62e9-7926-446b-8d9c-92cd370dfde8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c1032136-fcfe-48b6-8fd8-50710109416d", "AQAAAAEAACcQAAAAEI2807KZivOW91OCqtxPkYC8uC+rM6JbssZUlAFKVCga80vpSHygcH+C3y9FQipupw==", "d6c59d7c-86c2-4fcf-b8dd-eec1f578528d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Places");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "539e62e9-7926-446b-8d9c-92cd370dfde8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "abfc2ed5-2eb2-48aa-89be-2f34a2d9096f", "AQAAAAEAACcQAAAAEF7gjjN2w/bU89oyYB2UFOorpcHso1/0EDo+Gw0eLiOAy6CbdxfQpA9lLnL1qNWXsg==", "aabb7f13-a3e2-45b9-b98c-12d7b2c09fcc" });
        }
    }
}
