using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IdentityAspCore.Migrations
{
    /// <inheritdoc />
    public partial class addRoleToUserDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "164b69ef-72e6-491e-bdd8-81a878620a5b", "1", "Admin", "Admin" },
                    { "dd30fa8c-0fcc-440a-9f9f-8277652fea41", "3", "Guest", "Guest" },
                    { "fa363e90-947c-4631-a768-b84a8effb6e9", "2", "User", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "164b69ef-72e6-491e-bdd8-81a878620a5b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd30fa8c-0fcc-440a-9f9f-8277652fea41");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa363e90-947c-4631-a768-b84a8effb6e9");

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
    }
}
