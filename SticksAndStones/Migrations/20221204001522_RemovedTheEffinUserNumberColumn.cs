using Microsoft.EntityFrameworkCore.Migrations;

namespace SticksAndStones.Migrations
{
    public partial class RemovedTheEffinUserNumberColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f154e196-243c-4085-a4d9-ab399aa14527");

            migrationBuilder.DropColumn(
                name: "UserNumber",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "38bc7035-5fcb-452a-860d-1304f0c02bad", "8421391f-e322-4995-895b-6fc5e9d97808", "SiteAdmin", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "38bc7035-5fcb-452a-860d-1304f0c02bad");

            migrationBuilder.AddColumn<decimal>(
                name: "UserNumber",
                table: "AspNetUsers",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f154e196-243c-4085-a4d9-ab399aa14527", "3d09fee8-185e-4dd8-bc51-9c4e2094e8bb", "SiteAdmin", null });
        }
    }
}
