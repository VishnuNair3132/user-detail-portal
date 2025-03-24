using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IdentityAspCore.Migrations
{
    /// <inheritdoc />
    public partial class removeSuperAdminFromRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "135e9103-6636-471e-9b4e-5d0ee4a70279");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "76af660a-5111-4c5a-a28f-17dff4b9eb15");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f1740f6-39a3-420e-95cf-33808459baf2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc9d64e3-46f6-4e86-a535-aa91ebfe4505");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "20c1e6f1-f0a9-4f9f-a826-54b3794c7c64", "1", "Admin", "Admin" },
                    { "75a046c3-d1f7-4e6a-bf79-d523a1d5f554", "2", "User", "User" },
                    { "bec8b3b4-4935-45c3-b0f3-ce5004f7a768", "3", "Guest", "Guest" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "20c1e6f1-f0a9-4f9f-a826-54b3794c7c64");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "75a046c3-d1f7-4e6a-bf79-d523a1d5f554");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bec8b3b4-4935-45c3-b0f3-ce5004f7a768");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "135e9103-6636-471e-9b4e-5d0ee4a70279", "3", "Guest", "Guest" },
                    { "76af660a-5111-4c5a-a28f-17dff4b9eb15", "2", "User", "User" },
                    { "7f1740f6-39a3-420e-95cf-33808459baf2", "1", "Admin", "Admin" },
                    { "cc9d64e3-46f6-4e86-a535-aa91ebfe4505", "4", "SuperAdmin", "SuperAdmin" }
                });
        }
    }
}
