using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SticksAndStones.Migrations
{
    public partial class addedLeaderboard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Leaderboard",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    Ranking = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leaderboard", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Leaderboard_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Leaderboard",
                columns: new[] { "ID", "Date", "Ranking", "UserID" },
                values: new object[] { 1, new DateTime(2022, 10, 15, 20, 25, 48, 406, DateTimeKind.Local).AddTicks(3101), 1L, 1 });

            migrationBuilder.InsertData(
                table: "Leaderboard",
                columns: new[] { "ID", "Date", "Ranking", "UserID" },
                values: new object[] { 2, new DateTime(2022, 10, 15, 20, 25, 48, 411, DateTimeKind.Local).AddTicks(3064), 1L, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Leaderboard_UserID",
                table: "Leaderboard",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leaderboard");
        }
    }
}
