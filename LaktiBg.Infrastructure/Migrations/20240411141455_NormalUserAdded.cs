using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaktiBg.Infrastructure.Migrations
{
    public partial class NormalUserAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "539e62e9-7926-446b-8d9c-92cd370dfde8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4a78ed9c-a649-4b91-ac24-451e7aedd2b2", "AQAAAAEAACcQAAAAEIQ4UKG0oWihsCFpkY70fQ6dVdvk6jhoM74nphAoSRBtOc/osr2HX3I511+pcGpnIQ==", "fa04520e-0955-4291-8d38-6536b265d2e4" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "BirthDate", "ConcurrencyStamp", "Description", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Rating", "RegistrationDate", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "71368c9b-91fa-4338-bfce-e0921b5324ef", 0, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "d7456a3d-4a16-4134-9747-dec2bb1a5cf8", "Hi! I am an normal user account!", "normaluser@abv.bg", false, "Ivan", "Antonov", false, null, "NORMALUSER@ABV.BG", "NORMALUSER@ABV.BG", "AQAAAAEAACcQAAAAEMJ+gCn3KsbqJGs/giqw3220+lR5SqzAmnxa6H4HVk81AmQlwf9bzaYp5XdG78OJTQ==", "1234567890", false, 5m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "fbb491b2-879f-40ba-b72c-88ce79902bb0", false, "normaluser@abv.bg" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "71368c9b-91fa-4338-bfce-e0921b5324ef");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "539e62e9-7926-446b-8d9c-92cd370dfde8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c1032136-fcfe-48b6-8fd8-50710109416d", "AQAAAAEAACcQAAAAEI2807KZivOW91OCqtxPkYC8uC+rM6JbssZUlAFKVCga80vpSHygcH+C3y9FQipupw==", "d6c59d7c-86c2-4fcf-b8dd-eec1f578528d" });
        }
    }
}
