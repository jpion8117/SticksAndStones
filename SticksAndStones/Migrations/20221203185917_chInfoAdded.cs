using Microsoft.EntityFrameworkCore.Migrations;

namespace SticksAndStones.Migrations
{
    public partial class chInfoAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CharacterId",
                table: "Moves",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Flavortext",
                table: "Moves",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Flavortext",
                table: "Effects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Flavortext",
                table: "Characters",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MoveEffect",
                columns: table => new
                {
                    MoveId = table.Column<int>(nullable: false),
                    EffectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveEffect", x => new { x.MoveId, x.EffectId });
                    table.ForeignKey(
                        name: "FK_MoveEffect_Effects_EffectId",
                        column: x => x.EffectId,
                        principalTable: "Effects",
                        principalColumn: "EffectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoveEffect_Moves_MoveId",
                        column: x => x.MoveId,
                        principalTable: "Moves",
                        principalColumn: "MoveId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Moves_CharacterId",
                table: "Moves",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveEffect_EffectId",
                table: "MoveEffect",
                column: "EffectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Moves_Characters_CharacterId",
                table: "Moves",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "CharacterId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Moves_Characters_CharacterId",
                table: "Moves");

            migrationBuilder.DropTable(
                name: "MoveEffect");

            migrationBuilder.DropIndex(
                name: "IX_Moves_CharacterId",
                table: "Moves");

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "Moves");

            migrationBuilder.DropColumn(
                name: "Flavortext",
                table: "Moves");

            migrationBuilder.DropColumn(
                name: "Flavortext",
                table: "Effects");

            migrationBuilder.DropColumn(
                name: "Flavortext",
                table: "Characters");
        }
    }
}
