using Microsoft.EntityFrameworkCore.Migrations;

namespace SticksAndStones.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 30, nullable: false),
                    GamesPlayed = table.Column<int>(nullable: false),
                    GamesWon = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "GamesPlayed", "GamesWon", "UserName" },
                values: new object[] { 1, 10, 6, "jP" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "GamesPlayed", "GamesWon", "UserName" },
                values: new object[] { 2, 10, 4, "Max" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
