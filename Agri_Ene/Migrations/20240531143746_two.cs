using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Agri_Ene.Migrations
{
    /// <inheritdoc />
    public partial class two : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ba8535d-f406-4230-81ab-ec0178e0cc78");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "874cda08-fff6-4f9f-b6e8-0754ef6cb8cc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "40320b03-9365-46e0-8a3e-1a9b215c7b49", null, "farmer", "farmer" },
                    { "cc823b9f-53fa-4f5d-8ee9-d7a3b961c4ad", null, "employee", "employee" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "40320b03-9365-46e0-8a3e-1a9b215c7b49");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc823b9f-53fa-4f5d-8ee9-d7a3b961c4ad");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7ba8535d-f406-4230-81ab-ec0178e0cc78", null, "farmer", "farmer" },
                    { "874cda08-fff6-4f9f-b6e8-0754ef6cb8cc", null, "employee", "employee" }
                });
        }
    }
}
