using Microsoft.EntityFrameworkCore.Migrations;

namespace SticksAndStones.Migrations
{
    public partial class IdkIfINeedThis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoveEffect_Effects_EffectId",
                table: "MoveEffect");

            migrationBuilder.DropForeignKey(
                name: "FK_MoveEffect_Moves_MoveId",
                table: "MoveEffect");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MoveEffect",
                table: "MoveEffect");

            migrationBuilder.RenameTable(
                name: "MoveEffect",
                newName: "MoveEffects");

            migrationBuilder.RenameIndex(
                name: "IX_MoveEffect_EffectId",
                table: "MoveEffects",
                newName: "IX_MoveEffects_EffectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MoveEffects",
                table: "MoveEffects",
                columns: new[] { "MoveId", "EffectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MoveEffects_Effects_EffectId",
                table: "MoveEffects",
                column: "EffectId",
                principalTable: "Effects",
                principalColumn: "EffectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoveEffects_Moves_MoveId",
                table: "MoveEffects",
                column: "MoveId",
                principalTable: "Moves",
                principalColumn: "MoveId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoveEffects_Effects_EffectId",
                table: "MoveEffects");

            migrationBuilder.DropForeignKey(
                name: "FK_MoveEffects_Moves_MoveId",
                table: "MoveEffects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MoveEffects",
                table: "MoveEffects");

            migrationBuilder.RenameTable(
                name: "MoveEffects",
                newName: "MoveEffect");

            migrationBuilder.RenameIndex(
                name: "IX_MoveEffects_EffectId",
                table: "MoveEffect",
                newName: "IX_MoveEffect_EffectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MoveEffect",
                table: "MoveEffect",
                columns: new[] { "MoveId", "EffectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MoveEffect_Effects_EffectId",
                table: "MoveEffect",
                column: "EffectId",
                principalTable: "Effects",
                principalColumn: "EffectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoveEffect_Moves_MoveId",
                table: "MoveEffect",
                column: "MoveId",
                principalTable: "Moves",
                principalColumn: "MoveId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
