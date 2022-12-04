using Microsoft.EntityFrameworkCore.Migrations;

namespace SticksAndStones.Migrations
{
    public partial class userNavProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Taglines_AspNetUsers_AuthorizedByUserId",
                table: "Taglines");

            migrationBuilder.DropForeignKey(
                name: "FK_Taglines_AspNetUsers_SuggestedByUserId",
                table: "Taglines");

            migrationBuilder.DropIndex(
                name: "IX_Taglines_AuthorizedByUserId",
                table: "Taglines");

            migrationBuilder.DropIndex(
                name: "IX_Taglines_SuggestedByUserId",
                table: "Taglines");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "38bc7035-5fcb-452a-860d-1304f0c02bad");

            migrationBuilder.DropColumn(
                name: "AuthorizedByUserId",
                table: "Taglines");

            migrationBuilder.DropColumn(
                name: "SuggestedByUserId",
                table: "Taglines");

            migrationBuilder.AlterColumn<string>(
                name: "SuggestedById",
                table: "Taglines",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AuthorizedById",
                table: "Taglines",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateIndex(
                name: "IX_Taglines_AuthorizedById",
                table: "Taglines",
                column: "AuthorizedById");

            migrationBuilder.CreateIndex(
                name: "IX_Taglines_SuggestedById",
                table: "Taglines",
                column: "SuggestedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Taglines_AspNetUsers_AuthorizedById",
                table: "Taglines",
                column: "AuthorizedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Taglines_AspNetUsers_SuggestedById",
                table: "Taglines",
                column: "SuggestedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Taglines_AspNetUsers_AuthorizedById",
                table: "Taglines");

            migrationBuilder.DropForeignKey(
                name: "FK_Taglines_AspNetUsers_SuggestedById",
                table: "Taglines");

            migrationBuilder.DropIndex(
                name: "IX_Taglines_AuthorizedById",
                table: "Taglines");

            migrationBuilder.DropIndex(
                name: "IX_Taglines_SuggestedById",
                table: "Taglines");

            migrationBuilder.AlterColumn<string>(
                name: "SuggestedById",
                table: "Taglines",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AuthorizedById",
                table: "Taglines",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorizedByUserId",
                table: "Taglines",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SuggestedByUserId",
                table: "Taglines",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "38bc7035-5fcb-452a-860d-1304f0c02bad", "8421391f-e322-4995-895b-6fc5e9d97808", "SiteAdmin", null });

            migrationBuilder.CreateIndex(
                name: "IX_Taglines_AuthorizedByUserId",
                table: "Taglines",
                column: "AuthorizedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Taglines_SuggestedByUserId",
                table: "Taglines",
                column: "SuggestedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Taglines_AspNetUsers_AuthorizedByUserId",
                table: "Taglines",
                column: "AuthorizedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Taglines_AspNetUsers_SuggestedByUserId",
                table: "Taglines",
                column: "SuggestedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
