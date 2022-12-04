using Microsoft.EntityFrameworkCore.Migrations;

namespace SticksAndStones.Migrations
{
    public partial class LotsOfStufffs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ba4ffbf9-ec14-4f50-8e99-46959cbdcf15", "22b3ea11-f7c8-4799-ab49-dffba70f3794", "SiteAdmin", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba4ffbf9-ec14-4f50-8e99-46959cbdcf15");
        }
    }
}
