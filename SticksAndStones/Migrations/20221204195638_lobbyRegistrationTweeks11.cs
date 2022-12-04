using Microsoft.EntityFrameworkCore.Migrations;

namespace SticksAndStones.Migrations
{
    public partial class lobbyRegistrationTweeks11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GuestId",
                table: "Lobbies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HostId",
                table: "Lobbies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GuestId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HostId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lobbies_GuestId",
                table: "Lobbies",
                column: "GuestId",
                unique: true,
                filter: "[GuestId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Lobbies_HostId",
                table: "Lobbies",
                column: "HostId",
                unique: true,
                filter: "[HostId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Lobbies_AspNetUsers_GuestId",
                table: "Lobbies",
                column: "GuestId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lobbies_AspNetUsers_HostId",
                table: "Lobbies",
                column: "HostId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lobbies_AspNetUsers_GuestId",
                table: "Lobbies");

            migrationBuilder.DropForeignKey(
                name: "FK_Lobbies_AspNetUsers_HostId",
                table: "Lobbies");

            migrationBuilder.DropIndex(
                name: "IX_Lobbies_GuestId",
                table: "Lobbies");

            migrationBuilder.DropIndex(
                name: "IX_Lobbies_HostId",
                table: "Lobbies");

            migrationBuilder.DropColumn(
                name: "GuestId",
                table: "Lobbies");

            migrationBuilder.DropColumn(
                name: "HostId",
                table: "Lobbies");

            migrationBuilder.DropColumn(
                name: "GuestId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HostId",
                table: "AspNetUsers");
        }
    }
}
