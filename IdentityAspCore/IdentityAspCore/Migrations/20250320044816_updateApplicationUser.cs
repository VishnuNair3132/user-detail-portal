using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IdentityAspCore.Migrations
{
    /// <inheritdoc />
    public partial class updateApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Lastname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Lastname",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "AspNetUsers");

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
    }
}
