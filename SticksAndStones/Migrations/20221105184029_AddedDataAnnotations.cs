using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SticksAndStones.Migrations
{
    public partial class AddedDataAnnotations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "Username");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Leaderboard",
                keyColumn: "ID",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 11, 5, 14, 40, 28, 764, DateTimeKind.Local).AddTicks(9541));

            migrationBuilder.UpdateData(
                table: "Leaderboard",
                keyColumn: "ID",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2022, 11, 5, 14, 40, 28, 768, DateTimeKind.Local).AddTicks(2428));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "UserName");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.UpdateData(
                table: "Leaderboard",
                keyColumn: "ID",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 10, 15, 20, 25, 48, 406, DateTimeKind.Local).AddTicks(3101));

            migrationBuilder.UpdateData(
                table: "Leaderboard",
                keyColumn: "ID",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2022, 10, 15, 20, 25, 48, 411, DateTimeKind.Local).AddTicks(3064));
        }
    }
}
