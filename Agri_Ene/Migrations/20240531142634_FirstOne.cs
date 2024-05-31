using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Agri_Ene.Migrations
{
    public partial class FirstOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1adec4ea-25eb-4922-ba8d-b5ea133165ee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "666a255b-488c-4604-9ec3-88ea6abe43f4");

            // Use raw SQL commands to conditionally insert roles if they do not exist
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM AspNetRoles WHERE Name = 'farmer')
                BEGIN
                    INSERT INTO AspNetRoles (Id, ConcurrencyStamp, Name, NormalizedName)
                    VALUES ('7ba8535d-f406-4230-81ab-ec0178e0cc78', NULL, 'farmer', 'farmer')
                END
            ");

            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM AspNetRoles WHERE Name = 'employee')
                BEGIN
                    INSERT INTO AspNetRoles (Id, ConcurrencyStamp, Name, NormalizedName)
                    VALUES ('874cda08-fff6-4f9f-b6e8-0754ef6cb8cc', NULL, 'employee', 'employee')
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ba8535d-f406-4230-81ab-ec0178e0cc78");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "874cda08-fff6-4f9f-b6e8-0754ef6cb8cc");

            // Insert the original roles back
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1adec4ea-25eb-4922-ba8d-b5ea133165ee", null, "employee", "employee" },
                    { "666a255b-488c-4604-9ec3-88ea6abe43f4", null, "farmer", "farmer" }
                });
        }
    }
}
