using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IdentityAspCore.Migrations
{
    /// <inheritdoc />
    public partial class addSuperAdminToRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "27b5a229-edef-402b-9d54-459bd26159ea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ed2a262-018e-433d-9fe7-ceb4f9dd9bf5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de4041a0-317d-4afa-87bb-0a4e47159d9b");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "27b5a229-edef-402b-9d54-459bd26159ea", "1", "Admin", "Admin" },
                    { "5ed2a262-018e-433d-9fe7-ceb4f9dd9bf5", "2", "User", "User" },
                    { "de4041a0-317d-4afa-87bb-0a4e47159d9b", "3", "Guest", "Guest" }
                });
        }
    }
}
