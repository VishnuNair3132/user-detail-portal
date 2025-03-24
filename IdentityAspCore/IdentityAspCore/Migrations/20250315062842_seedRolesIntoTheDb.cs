using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IdentityAspCore.Migrations
{
    /// <inheritdoc />
    public partial class seedRolesIntoTheDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "07231ffb-d30b-4319-a72f-fdf1645a46fd", "2", "User", "User" },
                    { "9b2fa64f-f694-48b8-b53c-6b60b341c340", "1", "Admin", "Admin" },
                    { "d0a5421f-d1c5-4cbb-8adf-1c5ac1fda196", "3", "Guest", "Guest" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "07231ffb-d30b-4319-a72f-fdf1645a46fd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9b2fa64f-f694-48b8-b53c-6b60b341c340");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0a5421f-d1c5-4cbb-8adf-1c5ac1fda196");
        }
    }
}
