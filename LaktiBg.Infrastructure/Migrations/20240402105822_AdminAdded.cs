using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaktiBg.Infrastructure.Migrations
{
    public partial class AdminAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "BirthDate", "ConcurrencyStamp", "Description", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Rating", "RegistrationDate", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "539e62e9-7926-446b-8d9c-92cd370dfde8", 0, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "d49d022a-b028-4b73-b261-73952b755abe", "Admin account", "ApplicationUser", "admin@abv.bg", false, "Miroslav", "Atanasov", false, null, "ADMIN@ABV.BG", "ADMIN@ABV.BG", "AQAAAAEAACcQAAAAEEOTPA/C0RbfMza1n5wNA49pcXlOylhQ4jREk+G514qk0KomfVphesVt9bwdOKGfHQ==", null, false, 7m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "b340bd0f-de62-4d81-bc8c-b7c1dcbef822", false, "admin@abv.bg" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "539e62e9-7926-446b-8d9c-92cd370dfde8");
        }
    }
}
