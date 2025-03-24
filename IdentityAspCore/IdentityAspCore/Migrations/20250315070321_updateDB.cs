using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IdentityAspCore.Migrations
{
    /// <inheritdoc />
    public partial class updateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "70503cda-aafc-47a3-b8f9-a0d43856d8c3", "1", "Admin", "Admin" },
                    { "895efa97-ac00-4db6-9503-769ef7a644c6", "2", "User", "User" },
                    { "f96b77d9-b78b-4db2-b0b3-96497aa568ba", "3", "Guest", "Guest" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70503cda-aafc-47a3-b8f9-a0d43856d8c3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "895efa97-ac00-4db6-9503-769ef7a644c6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f96b77d9-b78b-4db2-b0b3-96497aa568ba");

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
    }
}
